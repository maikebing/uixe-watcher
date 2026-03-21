using Npgsql;
using NpgsqlTypes;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Infrastructure.TrafficEvents;

namespace Uixe.Copilot.Infrastructure.Persistence.TrafficEvents;

public sealed class PostgresTrafficEventRepository : ITrafficEventRepository
{
    private readonly string _connectionString;

    public PostgresTrafficEventRepository(InfrastructureOptions options)
    {
        _connectionString = options.TrafficEventPostgresConnectionString;
        Initialize().GetAwaiter().GetResult();
    }

    public async Task SaveAsync(TrafficEventPushRequestDto request, CancellationToken cancellationToken = default)
    {
        var entity = request.ToEntity();
        var record = entity.ToRecord();

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();
        command.CommandText = @"
INSERT INTO traffic_events (id, title, plaza_name, lane_no, level, status, image_url, video_url, image_urls_json, video_urls_json, occurred_at)
VALUES (@id, @title, @plaza_name, @lane_no, @level, @status, @image_url, @video_url, @image_urls_json, @video_urls_json, @occurred_at)
ON CONFLICT (id) DO UPDATE SET
    title = EXCLUDED.title,
    plaza_name = EXCLUDED.plaza_name,
    lane_no = EXCLUDED.lane_no,
    level = EXCLUDED.level,
    status = EXCLUDED.status,
    image_url = EXCLUDED.image_url,
    video_url = EXCLUDED.video_url,
    image_urls_json = EXCLUDED.image_urls_json,
    video_urls_json = EXCLUDED.video_urls_json,
    occurred_at = EXCLUDED.occurred_at;";

        AddCommonParameters(command, record);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<TrafficEventListItemDto>> GetRecentEventsAsync(CancellationToken cancellationToken = default)
    {
        return await QueryInternalAsync("SELECT * FROM traffic_events ORDER BY occurred_at DESC LIMIT 100;", cancellationToken);
    }

    public async Task<TrafficEventListItemDto?> GetByIdAsync(string eventId, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM traffic_events WHERE id = @id LIMIT 1;";
        command.Parameters.AddWithValue("id", eventId);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return DbTrafficEventStore.ReadItem(reader);
    }

    public async Task<IReadOnlyCollection<TrafficEventListItemDto>> QueryAsync(TrafficEventHistoryQueryDto query, CancellationToken cancellationToken = default)
    {
        var pageNo = query.PageNo <= 0 ? 1 : query.PageNo;
        var pageSize = query.PageSize <= 0 ? 20 : query.PageSize;

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();
        command.CommandText = $@"
SELECT * FROM traffic_events
{DbTrafficEventStore.BuildFilterWhereClause().Replace("@", ":")}
ORDER BY occurred_at DESC
LIMIT :limit OFFSET :offset;";

        AddFilterParameters(command, query, pageNo, pageSize);

        var items = new List<TrafficEventListItemDto>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(DbTrafficEventStore.ReadItem(reader));
        }

        return items.AsReadOnly();
    }

    public async Task<int> CountAsync(TrafficEventHistoryQueryDto query, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();
        command.CommandText = $@"
SELECT COUNT(1) FROM traffic_events
{DbTrafficEventStore.BuildFilterWhereClause().Replace("@", ":")};";

        AddFilterParameters(command, query, 1, 1, includePaging: false);
        var result = await command.ExecuteScalarAsync(cancellationToken);
        return Convert.ToInt32(result);
    }

    private async Task Initialize()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = connection.CreateCommand();
        command.CommandText = @"
CREATE TABLE IF NOT EXISTS traffic_events (
    id text PRIMARY KEY,
    title text NOT NULL,
    plaza_name text NOT NULL,
    lane_no text NOT NULL,
    level text NOT NULL,
    status text NOT NULL,
    image_url text NULL,
    video_url text NULL,
    image_urls_json text NOT NULL,
    video_urls_json text NOT NULL,
    occurred_at timestamptz NOT NULL
);";

        await command.ExecuteNonQueryAsync();
    }

    private async Task<IReadOnlyCollection<TrafficEventListItemDto>> QueryInternalAsync(string sql, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();
        command.CommandText = sql;

        var items = new List<TrafficEventListItemDto>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(DbTrafficEventStore.ReadItem(reader));
        }

        return items.AsReadOnly();
    }

    private static void AddCommonParameters(NpgsqlCommand command, TrafficEventRecord record)
    {
        command.Parameters.AddWithValue("id", record.Id);
        command.Parameters.AddWithValue("title", record.Title);
        command.Parameters.AddWithValue("plaza_name", record.PlazaName);
        command.Parameters.AddWithValue("lane_no", record.LaneNo);
        command.Parameters.AddWithValue("level", record.Level);
        command.Parameters.AddWithValue("status", record.Status);
        command.Parameters.AddWithValue("image_url", (object?)record.ImageUrl ?? DBNull.Value);
        command.Parameters.AddWithValue("video_url", (object?)record.VideoUrl ?? DBNull.Value);
        command.Parameters.AddWithValue("image_urls_json", record.ImageUrlsJson);
        command.Parameters.AddWithValue("video_urls_json", record.VideoUrlsJson);
        command.Parameters.AddWithValue("occurred_at", NpgsqlDbType.TimestampTz, record.OccurredAt);
    }

    private static void AddFilterParameters(NpgsqlCommand command, TrafficEventHistoryQueryDto query, int pageNo, int pageSize, bool includePaging = true)
    {
        command.Parameters.AddWithValue("startTime", (object?)query.StartTime ?? DBNull.Value);
        command.Parameters.AddWithValue("endTime", (object?)query.EndTime ?? DBNull.Value);
        command.Parameters.AddWithValue("plazaName", query.PlazaName ?? string.Empty);
        command.Parameters.AddWithValue("eventType", query.EventType ?? string.Empty);
        command.Parameters.AddWithValue("status", query.Status ?? string.Empty);
        command.Parameters.AddWithValue("plazaLike", $"%{query.PlazaName ?? string.Empty}%");
        command.Parameters.AddWithValue("eventLike", $"%{query.EventType ?? string.Empty}%");
        command.Parameters.AddWithValue("statusLike", $"%{query.Status ?? string.Empty}%");

        if (includePaging)
        {
            command.Parameters.AddWithValue("limit", pageSize);
            command.Parameters.AddWithValue("offset", (pageNo - 1) * pageSize);
        }
    }
}