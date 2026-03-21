using System.Text.Json;
using Uixe.Copilot.Domain.Entities;

namespace Uixe.Copilot.Infrastructure.Persistence.TrafficEvents;

internal static class TrafficEventPersistenceMappingExtensions
{
    public static TrafficEventRecord ToRecord(this TrafficEvent entity)
    {
        return new TrafficEventRecord
        {
            Id = entity.Id,
            Title = entity.Title,
            PlazaName = entity.PlazaName,
            LaneNo = entity.LaneNo,
            Level = entity.Level,
            Status = entity.Status,
            ImageUrl = entity.ImageUrl,
            VideoUrl = entity.VideoUrl,
            ImageUrlsJson = JsonSerializer.Serialize(entity.ImageUrls),
            VideoUrlsJson = JsonSerializer.Serialize(entity.VideoUrls),
            OccurredAt = entity.OccurredAt
        };
    }
}