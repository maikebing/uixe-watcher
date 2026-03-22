using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Contracts.Responses;

namespace Uixe.Copilot.Application.Services;

public sealed class NoOpLegacyPlazaUiBridge : ILegacyPlazaUiBridge
{
    public Task<ApiResult> ShowLaneStatusAsync(string plazaId, string laneNo, LaneStatusDto status, CancellationToken cancellationToken = default)
        => Task.FromResult(new ApiResult(ApiCode.OK, $"Lane status ignored for {plazaId}/{laneNo}"));

    public Task<ApiResult> ShowLaneLostAsync(string plazaId, string laneNo, CancellationToken cancellationToken = default)
        => Task.FromResult(new ApiResult(ApiCode.OK, $"Lane lost ignored for {plazaId}/{laneNo}"));

    public Task<ApiResult> ShowMessageAsync(string plazaId, LaneMessageDto message, CancellationToken cancellationToken = default)
        => Task.FromResult(new ApiResult(ApiCode.OK, $"Message ignored for {plazaId}"));

    public Task<ApiResult> ShowBulkTransportAsync(string plazaId, BulkTransportDto dto, CancellationToken cancellationToken = default)
        => Task.FromResult(new ApiResult(ApiCode.OK, $"Bulk transport ignored for {plazaId}"));

    public Task<ApiResult> ShowBillInfoAsync(string plazaId, BillInfoRequestDto dto, CancellationToken cancellationToken = default)
        => Task.FromResult(new ApiResult(ApiCode.OK, $"Bill info ignored for {plazaId}"));

    public Task<ApiResult> ShowConfirmEnInfoAsync(string plazaId, ConfirmEnInfoDto dto, CancellationToken cancellationToken = default)
        => Task.FromResult(new ApiResult(ApiCode.OK, $"Confirm entry ignored for {plazaId}"));

    public Task<ApiResult> ShowOverloadAlarmAsync(string plazaId, OverloadWarningDto warning, bool playSpeech, CancellationToken cancellationToken = default)
        => Task.FromResult(new ApiResult(ApiCode.OK, $"Overload alarm ignored for {plazaId}"));

    public Task<ApiResult> ShowLaneSpecialAsync(string plazaId, LaneSpecialDto message, CancellationToken cancellationToken = default)
        => Task.FromResult(new ApiResult(ApiCode.OK, $"Lane special ignored for {plazaId}"));

    public void PlayAlertRing()
    {
    }

    public TrafficEventTargetResolutionResult? TryResolveTrafficEventTarget(TrafficEventPushRequestDto request)
        => null;
}