using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Contracts.Responses;

namespace Uixe.Copilot.Application.Abstractions;

public interface ILaneApplicationService
{
    Task<ApiResult> ShowLaneStatusAsync(string plazaId, string laneNo, object status, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowWeightMessageAsync(string plazaId, object message, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowTcoConfirmAsync(string plazaId, object message, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowMessageAsync(string plazaId, object message, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowOverloadAlarmAsync(string plazaId, string title, string context, bool playSpeech, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowLaneSpecialAsync(string plazaId, object message, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowBulkTransAsync(string plazaId, object dto, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowBillInfoAsync(string plazaId, object dto, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowConfirmEnInfoAsync(string plazaId, object dto, CancellationToken cancellationToken = default);

    Task<TrafficEventPushResponse> EnqueueTrafficEventAsync(TrafficEventPushRequestDto request, CancellationToken cancellationToken = default);
}
