using Uixe.Copilot.Contracts.Responses;
using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Application.Abstractions;

public sealed class TrafficEventTargetResolutionResult
{
    public Func<CancellationToken, Task>? DisplayAction { get; set; }

    public object? Plaza { get; set; }

    public object? Lane { get; set; }

    public object? FormRequest { get; set; }
}

public interface ILegacyPlazaUiBridge
{
    Task<ApiResult> ShowLaneStatusAsync(string plazaId, string laneNo, LaneStatusDto status, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowLaneLostAsync(string plazaId, string laneNo, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowMessageAsync(string plazaId, LaneMessageDto message, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowBulkTransportAsync(string plazaId, BulkTransportDto dto, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowBillInfoAsync(string plazaId, BillInfoRequestDto dto, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowConfirmEnInfoAsync(string plazaId, ConfirmEnInfoDto dto, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowOverloadAlarmAsync(string plazaId, OverloadWarningDto warning, bool playSpeech, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowLaneSpecialAsync(string plazaId, LaneSpecialDto message, CancellationToken cancellationToken = default);

    void PlayAlertRing();

    TrafficEventTargetResolutionResult? TryResolveTrafficEventTarget(TrafficEventPushRequestDto request);
}