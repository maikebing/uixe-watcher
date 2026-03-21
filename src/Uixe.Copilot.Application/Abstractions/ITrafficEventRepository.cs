using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Application.Abstractions;

public interface ITrafficEventRepository
{
    Task SaveAsync(TrafficEventPushRequestDto request, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<TrafficEventListItemDto>> GetRecentEventsAsync(CancellationToken cancellationToken = default);

    Task<TrafficEventListItemDto?> GetByIdAsync(string eventId, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<TrafficEventListItemDto>> QueryAsync(TrafficEventHistoryQueryDto query, CancellationToken cancellationToken = default);
}