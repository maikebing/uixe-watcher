using System.Collections.Concurrent;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Infrastructure.TrafficEvents;

public sealed class InMemoryTrafficEventRepository : ITrafficEventRepository
{
    private readonly ConcurrentDictionary<string, Uixe.Copilot.Domain.Entities.TrafficEvent> _events = new(StringComparer.OrdinalIgnoreCase);

    public Task SaveAsync(TrafficEventPushRequestDto request, CancellationToken cancellationToken = default)
    {
        var entity = request.ToEntity();
        _events[entity.Id] = entity;
        return Task.CompletedTask;
    }

    public Task<IReadOnlyCollection<TrafficEventListItemDto>> GetRecentEventsAsync(CancellationToken cancellationToken = default)
    {
        var items = _events.Values
            .OrderByDescending(x => x.OccurredAt)
            .Select(x => x.ToListItemDto())
            .ToList()
            .AsReadOnly();

        return Task.FromResult<IReadOnlyCollection<TrafficEventListItemDto>>(items);
    }

    public Task<TrafficEventListItemDto?> GetByIdAsync(string eventId, CancellationToken cancellationToken = default)
    {
        _events.TryGetValue(eventId, out var entity);
        return Task.FromResult(entity?.ToListItemDto());
    }

    public Task<IReadOnlyCollection<TrafficEventListItemDto>> QueryAsync(TrafficEventHistoryQueryDto query, CancellationToken cancellationToken = default)
    {
        var pageNo = query.PageNo <= 0 ? 1 : query.PageNo;
        var pageSize = query.PageSize <= 0 ? 20 : query.PageSize;

        var items = ApplyFilter(_events.Values, query)
            .Where(x => !query.StartTime.HasValue || x.OccurredAt >= query.StartTime.Value)
            .Where(x => !query.EndTime.HasValue || x.OccurredAt <= query.EndTime.Value)
            .Select(x => x.ToListItemDto())
            .OrderByDescending(x => x.Time)
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .ToList()
            .AsReadOnly();

        return Task.FromResult<IReadOnlyCollection<TrafficEventListItemDto>>(items);
    }

    public Task<int> CountAsync(TrafficEventHistoryQueryDto query, CancellationToken cancellationToken = default)
    {
        var total = ApplyFilter(_events.Values, query).Count();
        return Task.FromResult(total);
    }

    private static IEnumerable<Uixe.Copilot.Domain.Entities.TrafficEvent> ApplyFilter(IEnumerable<Uixe.Copilot.Domain.Entities.TrafficEvent> events, TrafficEventHistoryQueryDto query)
    {
        return events
            .Where(x => !query.StartTime.HasValue || x.OccurredAt >= query.StartTime.Value)
            .Where(x => !query.EndTime.HasValue || x.OccurredAt <= query.EndTime.Value)
            .Where(x => string.IsNullOrWhiteSpace(query.PlazaName) || x.PlazaName.Contains(query.PlazaName, StringComparison.OrdinalIgnoreCase))
            .Where(x => string.IsNullOrWhiteSpace(query.EventType) || x.Title.Contains(query.EventType, StringComparison.OrdinalIgnoreCase))
            .Where(x => string.IsNullOrWhiteSpace(query.Status) || x.Status.Contains(query.Status, StringComparison.OrdinalIgnoreCase));
    }
}