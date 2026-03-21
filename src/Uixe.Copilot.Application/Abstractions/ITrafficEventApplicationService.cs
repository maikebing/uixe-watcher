using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Contracts.Responses;

namespace Uixe.Copilot.Application.Abstractions;

public interface ITrafficEventApplicationService
{
    Task<TrafficEventPushResponse> SubmitAsync(TrafficEventPushRequestDto request, CancellationToken cancellationToken = default);
}
