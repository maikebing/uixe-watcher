using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Contracts.Responses;

namespace Uixe.Copilot.Application.Abstractions;

public interface ILaneApplicationService
{
    Task<ApiResult> ShowLaneStatusAsync(string plazaId, string laneNo, LaneStatusDto status, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowLaneLostAsync(string plazaId, string laneNo, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowWeightMessageAsync(string plazaId, TcoWeightMessageDto message, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowTcoConfirmAsync(string plazaId, TcoConfirmRequestDto message, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowMessageAsync(string plazaId, LaneMessageDto message, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowOverloadAlarmAsync(string plazaId, OverloadWarningDto warning, bool playSpeech, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowLaneSpecialAsync(string plazaId, LaneSpecialDto message, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowBulkTransAsync(string plazaId, BulkTransportDto dto, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowBillInfoAsync(string plazaId, BillInfoRequestDto dto, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowConfirmEnInfoAsync(string plazaId, ConfirmEnInfoDto dto, CancellationToken cancellationToken = default);

    Task<TrafficEventPushResponse> EnqueueTrafficEventAsync(TrafficEventPushRequestDto request, CancellationToken cancellationToken = default);
}
