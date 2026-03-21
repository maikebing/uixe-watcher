using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Application.Services;

public sealed class NoOpRealtimePushService : IRealtimePushService
{
    public Task PublishTrafficEventSubmittedAsync(TrafficEventPushRequestDto request, CancellationToken cancellationToken = default)
        => Task.CompletedTask;
}
