using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Domain.Entities;

namespace Uixe.Copilot.Infrastructure.TrafficEvents;

internal static class TrafficEventMappingExtensions
{
    public static TrafficEvent ToEntity(this TrafficEventPushRequestDto request)
    {
        var occurredAt = request.CapTime ?? request.StartTime ?? DateTime.Now;

        return new TrafficEvent
        {
            Id = string.IsNullOrWhiteSpace(request.RecordId) ? Guid.NewGuid().ToString("N") : request.RecordId,
            Title = string.IsNullOrWhiteSpace(request.EventType) ? "交通事件" : request.EventType,
            PlazaName = string.IsNullOrWhiteSpace(request.LaneNo) ? "未知收费站" : $"车道 {request.LaneNo}",
            LaneNo = request.LaneNo ?? string.Empty,
            Level = ResolveLevel(request.EventType),
            Status = "待处理",
            ImageUrl = ResolveFirstMedia(request.ImageList),
            VideoUrl = ResolveFirstMedia(request.VideoList),
            ImageUrls = ResolveMediaList(request.ImageList),
            VideoUrls = ResolveMediaList(request.VideoList),
            OccurredAt = occurredAt
        };
    }

    public static TrafficEventListItemDto ToListItemDto(this TrafficEvent entity)
    {
        return new TrafficEventListItemDto
        {
            Id = entity.Id,
            Title = entity.Title,
            PlazaName = entity.PlazaName,
            LaneNo = entity.LaneNo,
            Level = entity.Level,
            Time = entity.OccurredAt.ToString("HH:mm:ss"),
            Status = entity.Status,
            ImageUrl = entity.ImageUrl,
            VideoUrl = entity.VideoUrl,
            ImageUrls = entity.ImageUrls,
            VideoUrls = entity.VideoUrls
        };
    }

    private static List<string> ResolveMediaList(string? raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
        {
            return new List<string>();
        }

        return raw
            .Split(new[] { ',', ';', '|', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .ToList();
    }

    private static string? ResolveFirstMedia(string? raw)
    {
        return ResolveMediaList(raw).FirstOrDefault();
    }

    private static string ResolveLevel(string? eventType)
    {
        if (string.IsNullOrWhiteSpace(eventType))
        {
            return "medium";
        }

        if (eventType.Contains("告警", StringComparison.OrdinalIgnoreCase) || eventType.Contains("排队", StringComparison.OrdinalIgnoreCase))
        {
            return "high";
        }

        if (eventType.Contains("确认", StringComparison.OrdinalIgnoreCase))
        {
            return "medium";
        }

        return "low";
    }
}