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
        var items = _events.Values
            .Select(x => x.ToListItemDto())
            .Where(x => string.IsNullOrWhiteSpace(query.PlazaName) || x.PlazaName.Contains(query.PlazaName, StringComparison.OrdinalIgnoreCase))
            .Where(x => string.IsNullOrWhiteSpace(query.EventType) || x.Title.Contains(query.EventType, StringComparison.OrdinalIgnoreCase))
            .Where(x => string.IsNullOrWhiteSpace(query.Status) || x.Status.Contains(query.Status, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(x => x.Time)
            .ToList()
            .AsReadOnly();

        return Task.FromResult<IReadOnlyCollection<TrafficEventListItemDto>>(items);
    }
}