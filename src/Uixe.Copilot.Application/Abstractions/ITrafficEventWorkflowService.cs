using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Application.Abstractions;

public interface ITrafficEventWorkflowService
{
    Task<bool> EnqueueAsync(TrafficEventPushRequestDto request, IReadOnlyCollection<PlazaInfo> plazas, CancellationToken cancellationToken = default);
}
