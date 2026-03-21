using System.Collections.Concurrent;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Application.Services;

public sealed class InMemoryTrafficEventRepository : ITrafficEventRepository
{
    private readonly ConcurrentDictionary<string, TrafficEventListItemDto> _events = new(StringComparer.OrdinalIgnoreCase);

    public Task SaveAsync(TrafficEventPushRequestDto request, CancellationToken cancellationToken = default)
    {
        var id = string.IsNullOrWhiteSpace(request.RecordId) ? Guid.NewGuid().ToString("N") : request.RecordId;
        var eventItem = new TrafficEventListItemDto
        {
            Id = id,
            Title = string.IsNullOrWhiteSpace(request.EventType) ? "交通事件" : request.EventType,
            PlazaName = ResolvePlazaName(request),
            LaneNo = request.LaneNo ?? string.Empty,
            Level = ResolveLevel(request.EventType),
            Time = (request.CapTime ?? request.StartTime ?? DateTime.Now).ToString("HH:mm:ss"),
            Status = "待处理"
        };

        _events[id] = eventItem;
        return Task.CompletedTask;
    }

    public Task<IReadOnlyCollection<TrafficEventListItemDto>> GetRecentEventsAsync(CancellationToken cancellationToken = default)
    {
        var items = _events.Values
            .OrderByDescending(x => x.Time)
            .ToList()
            .AsReadOnly();

        return Task.FromResult<IReadOnlyCollection<TrafficEventListItemDto>>(items);
    }

    public Task<TrafficEventListItemDto?> GetByIdAsync(string eventId, CancellationToken cancellationToken = default)
    {
        _events.TryGetValue(eventId, out var eventItem);
        return Task.FromResult(eventItem);
    }

    private static string ResolvePlazaName(TrafficEventPushRequestDto request)
    {
        return string.IsNullOrWhiteSpace(request.LaneNo) ? "未知收费站" : $"车道 {request.LaneNo}";
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