using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Application.Abstractions;

public interface ITrafficEventQueryService
{
    Task<TrafficEventOverviewDto> GetOverviewAsync(CancellationToken cancellationToken = default);

    Task<TrafficEventListItemDto?> GetByIdAsync(string eventId, CancellationToken cancellationToken = default);
}
