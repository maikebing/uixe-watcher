using Microsoft.Data.Sqlite;
using System.Text.Json;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Domain.Entities;
using Uixe.Copilot.Infrastructure.TrafficEvents;

namespace Uixe.Copilot.Infrastructure.Persistence.TrafficEvents;

public sealed class DatabaseTrafficEventRepository : ITrafficEventRepository
{
    private readonly string _connectionString;

    public DatabaseTrafficEventRepository(InfrastructureOptions options)
    {
        _connectionString = NormalizeConnectionString(options.TrafficEventConnectionString);
        Initialize().GetAwaiter().GetResult();
    }

    public async Task SaveAsync(TrafficEventPushRequestDto request, CancellationToken cancellationToken = default)
    {
        var entity = request.ToEntity();

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var command = connection.CreateCommand();
        command.CommandText = @"
INSERT INTO TrafficEvents (Id, Title, PlazaName, LaneNo, Level, Status, ImageUrl, VideoUrl, ImageUrlsJson, VideoUrlsJson, OccurredAt)
VALUES ($id, $title, $plazaName, $laneNo, $level, $status, $imageUrl, $videoUrl, $imageUrlsJson, $videoUrlsJson, $occurredAt)
ON CONFLICT(Id) DO UPDATE SET
    Title = excluded.Title,
    PlazaName = excluded.PlazaName,
    LaneNo = excluded.LaneNo,
    Level = excluded.Level,
    Status = excluded.Status,
    ImageUrl = excluded.ImageUrl,
    VideoUrl = excluded.VideoUrl,
    ImageUrlsJson = excluded.ImageUrlsJson,
    VideoUrlsJson = excluded.VideoUrlsJson,
    OccurredAt = excluded.OccurredAt;";

        var record = entity.ToRecord();
        command.Parameters.AddWithValue("$id", record.Id);
        command.Parameters.AddWithValue("$title", record.Title);
        command.Parameters.AddWithValue("$plazaName", record.PlazaName);
        command.Parameters.AddWithValue("$laneNo", record.LaneNo);
        command.Parameters.AddWithValue("$level", record.Level);
        command.Parameters.AddWithValue("$status", record.Status);
        command.Parameters.AddWithValue("$imageUrl", (object?)record.ImageUrl ?? DBNull.Value);
        command.Parameters.AddWithValue("$videoUrl", (object?)record.VideoUrl ?? DBNull.Value);
        command.Parameters.AddWithValue("$imageUrlsJson", record.ImageUrlsJson);
        command.Parameters.AddWithValue("$videoUrlsJson", record.VideoUrlsJson);
        command.Parameters.AddWithValue("$occurredAt", record.OccurredAt.ToString("O"));

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<TrafficEventListItemDto>> GetRecentEventsAsync(CancellationToken cancellationToken = default)
    {
        var items = await QueryInternalAsync(
            @"SELECT * FROM TrafficEvents ORDER BY OccurredAt DESC LIMIT 100;",
            cancellationToken);
        return items;
    }

    public async Task<TrafficEventListItemDto?> GetByIdAsync(string eventId, CancellationToken cancellationToken = default)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM TrafficEvents WHERE Id = $id LIMIT 1;";
        command.Parameters.AddWithValue("$id", eventId);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return ReadItem(reader);
    }

    public async Task<IReadOnlyCollection<TrafficEventListItemDto>> QueryAsync(TrafficEventHistoryQueryDto query, CancellationToken cancellationToken = default)
    {
        var pageNo = query.PageNo <= 0 ? 1 : query.PageNo;
        var pageSize = query.PageSize <= 0 ? 20 : query.PageSize;

        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var command = connection.CreateCommand();
        command.CommandText = @"
SELECT * FROM TrafficEvents
WHERE ($startTime IS NULL OR OccurredAt >= $startTime)
  AND ($endTime IS NULL OR OccurredAt <= $endTime)
  AND ($plazaName = '' OR PlazaName LIKE $plazaLike)
  AND ($eventType = '' OR Title LIKE $eventLike)
  AND ($status = '' OR Status LIKE $statusLike)
ORDER BY OccurredAt DESC
LIMIT $limit OFFSET $offset;";

        command.Parameters.AddWithValue("$startTime", query.StartTime?.ToString("O") ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("$endTime", query.EndTime?.ToString("O") ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("$plazaName", query.PlazaName ?? string.Empty);
        command.Parameters.AddWithValue("$eventType", query.EventType ?? string.Empty);
        command.Parameters.AddWithValue("$status", query.Status ?? string.Empty);
        command.Parameters.AddWithValue("$plazaLike", $"%{query.PlazaName ?? string.Empty}%");
        command.Parameters.AddWithValue("$eventLike", $"%{query.EventType ?? string.Empty}%");
        command.Parameters.AddWithValue("$statusLike", $"%{query.Status ?? string.Empty}%");
        command.Parameters.AddWithValue("$limit", pageSize);
        command.Parameters.AddWithValue("$offset", (pageNo - 1) * pageSize);

        var items = new List<TrafficEventListItemDto>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(ReadItem(reader));
        }

        return items.AsReadOnly();
    }

    private async Task Initialize()
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        var command = connection.CreateCommand();
        command.CommandText = @"
CREATE TABLE IF NOT EXISTS TrafficEvents (
    Id TEXT PRIMARY KEY,
    Title TEXT NOT NULL,
    PlazaName TEXT NOT NULL,
    LaneNo TEXT NOT NULL,
    Level TEXT NOT NULL,
    Status TEXT NOT NULL,
    ImageUrl TEXT NULL,
    VideoUrl TEXT NULL,
    ImageUrlsJson TEXT NOT NULL,
    VideoUrlsJson TEXT NOT NULL,
    OccurredAt TEXT NOT NULL
);";

        await command.ExecuteNonQueryAsync();
    }

    private async Task<IReadOnlyCollection<TrafficEventListItemDto>> QueryInternalAsync(string sql, CancellationToken cancellationToken)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        var command = connection.CreateCommand();
        command.CommandText = sql;

        var items = new List<TrafficEventListItemDto>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(ReadItem(reader));
        }

        return items.AsReadOnly();
    }

    private static TrafficEventListItemDto ReadItem(SqliteDataReader reader)
    {
        return new TrafficEventListItemDto
        {
            Id = reader.GetString(reader.GetOrdinal("Id")),
            Title = reader.GetString(reader.GetOrdinal("Title")),
            PlazaName = reader.GetString(reader.GetOrdinal("PlazaName")),
            LaneNo = reader.GetString(reader.GetOrdinal("LaneNo")),
            Level = reader.GetString(reader.GetOrdinal("Level")),
            Status = reader.GetString(reader.GetOrdinal("Status")),
            ImageUrl = reader.IsDBNull(reader.GetOrdinal("ImageUrl")) ? null : reader.GetString(reader.GetOrdinal("ImageUrl")),
            VideoUrl = reader.IsDBNull(reader.GetOrdinal("VideoUrl")) ? null : reader.GetString(reader.GetOrdinal("VideoUrl")),
            ImageUrls = JsonSerializer.Deserialize<List<string>>(reader.GetString(reader.GetOrdinal("ImageUrlsJson"))) ?? new List<string>(),
            VideoUrls = JsonSerializer.Deserialize<List<string>>(reader.GetString(reader.GetOrdinal("VideoUrlsJson"))) ?? new List<string>(),
            Time = DateTime.Parse(reader.GetString(reader.GetOrdinal("OccurredAt"))).ToString("HH:mm:ss")
        };
    }

    private static string NormalizeConnectionString(string connectionString)
    {
        var builder = new SqliteConnectionStringBuilder(connectionString);
        if (string.IsNullOrWhiteSpace(builder.DataSource))
        {
            return connectionString;
        }

        var fullPath = Path.GetFullPath(builder.DataSource);
        var directory = Path.GetDirectoryName(fullPath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        builder.DataSource = fullPath;
        return builder.ToString();
    }
}