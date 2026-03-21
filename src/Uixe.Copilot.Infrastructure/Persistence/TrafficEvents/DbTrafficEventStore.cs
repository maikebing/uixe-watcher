using System.Data.Common;
using System.Text.Json;
using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Infrastructure.Persistence.TrafficEvents;

internal static class DbTrafficEventStore
{
    public static TrafficEventListItemDto ReadItem(DbDataReader reader)
    {
        return new TrafficEventListItemDto
        {
            Id = reader["Id"].ToString() ?? string.Empty,
            Title = reader["Title"].ToString() ?? string.Empty,
            PlazaName = reader["PlazaName"].ToString() ?? string.Empty,
            LaneNo = reader["LaneNo"].ToString() ?? string.Empty,
            Level = reader["Level"].ToString() ?? string.Empty,
            Status = reader["Status"].ToString() ?? string.Empty,
            ImageUrl = reader["ImageUrl"] as string,
            VideoUrl = reader["VideoUrl"] as string,
            ImageUrls = JsonSerializer.Deserialize<List<string>>(reader["ImageUrlsJson"]?.ToString() ?? "[]") ?? new List<string>(),
            VideoUrls = JsonSerializer.Deserialize<List<string>>(reader["VideoUrlsJson"]?.ToString() ?? "[]") ?? new List<string>(),
            Time = DateTime.Parse(reader["OccurredAt"].ToString() ?? DateTime.Now.ToString("O")).ToString("HH:mm:ss")
        };
    }

    public static string BuildFilterWhereClause() => @"
WHERE (@startTime IS NULL OR OccurredAt >= @startTime)
  AND (@endTime IS NULL OR OccurredAt <= @endTime)
  AND (@plazaName = '' OR PlazaName LIKE @plazaLike)
  AND (@eventType = '' OR Title LIKE @eventLike)
  AND (@status = '' OR Status LIKE @statusLike)";
}