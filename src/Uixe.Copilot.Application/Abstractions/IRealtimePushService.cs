using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Application.Abstractions;

public interface IRealtimePushService
{
    Task PublishTrafficEventSubmittedAsync(TrafficEventPushRequestDto request, CancellationToken cancellationToken = default);
}
