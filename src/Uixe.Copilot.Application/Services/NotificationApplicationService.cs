using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Responses;

namespace Uixe.Copilot.Application.Services;

public sealed class NotificationApplicationService : INotificationApplicationService
{
    public Task<ApiResult> ShowWeightMessageAsync(string plazaId, object message, CancellationToken cancellationToken = default)
        => Task.FromResult(new ApiResult(ApiCode.OK, $"Weight message queued for {plazaId}"));

    public Task<ApiResult> ShowTcoConfirmAsync(string plazaId, object message, CancellationToken cancellationToken = default)
        => Task.FromResult(new ApiResult(ApiCode.OK, $"TCO confirm queued for {plazaId}"));

    public Task<ApiResult> ShowOverloadAlarmAsync(string plazaId, string title, string context, bool playSpeech, CancellationToken cancellationToken = default)
        => Task.FromResult(new ApiResult(ApiCode.OK, $"Overload alarm queued for {plazaId}"));

    public Task<ApiResult> ShowLaneSpecialAsync(string plazaId, object message, CancellationToken cancellationToken = default)
        => Task.FromResult(new ApiResult(ApiCode.OK, $"Lane special queued for {plazaId}"));
}
