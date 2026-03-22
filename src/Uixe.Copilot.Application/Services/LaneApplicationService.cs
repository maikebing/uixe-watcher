using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Contracts.Responses;
namespace Uixe.Copilot.Application.Services;

public sealed class LaneApplicationService : ILaneApplicationService
{
    private readonly IPlazaContextService _plazaContextService;
    private readonly ILegacyPlazaUiBridge _legacyPlazaUiBridge;
    private readonly ITrafficEventApplicationService _trafficEventApplicationService;
    private readonly INotificationApplicationService _notificationApplicationService;
    private readonly ITcoWindowApplicationService _tcoWindowApplicationService;
    private readonly ILegacyTcoInteractionService _legacyTcoInteractionService;

    public LaneApplicationService(
        IPlazaContextService plazaContextService,
        ILegacyPlazaUiBridge legacyPlazaUiBridge,
        ITrafficEventApplicationService trafficEventApplicationService,
        INotificationApplicationService notificationApplicationService,
        ITcoWindowApplicationService tcoWindowApplicationService,
        ILegacyTcoInteractionService legacyTcoInteractionService)
    {
        _plazaContextService = plazaContextService;
        _legacyPlazaUiBridge = legacyPlazaUiBridge;
        _trafficEventApplicationService = trafficEventApplicationService;
        _notificationApplicationService = notificationApplicationService;
        _tcoWindowApplicationService = tcoWindowApplicationService;
        _legacyTcoInteractionService = legacyTcoInteractionService;
    }

    public Task<ApiResult> ShowLaneStatusAsync(string plazaId, string laneNo, LaneStatusDto status, CancellationToken cancellationToken = default)
        => _legacyPlazaUiBridge.ShowLaneStatusAsync(plazaId, laneNo, status, cancellationToken);

    public async Task<ApiResult> ShowWeightMessageAsync(string plazaId, TcoWeightMessageDto message, CancellationToken cancellationToken = default)
    {
        await _notificationApplicationService.ShowWeightMessageAsync(plazaId, message, cancellationToken);
        await _tcoWindowApplicationService.ShowWeightMessageAsync(plazaId, message, cancellationToken);
        await _legacyTcoInteractionService.ShowWeightMessageAsync(plazaId, message, cancellationToken);
        _legacyPlazaUiBridge.PlayAlertRing();
        return new ApiResult(ApiCode.OK, "OK");
    }

    public async Task<ApiResult> ShowTcoConfirmAsync(string plazaId, TcoConfirmRequestDto message, CancellationToken cancellationToken = default)
    {
        await _notificationApplicationService.ShowTcoConfirmAsync(plazaId, message, cancellationToken);
        await _tcoWindowApplicationService.ShowTcoConfirmAsync(plazaId, message, cancellationToken);
        await _legacyTcoInteractionService.ShowTcoConfirmAsync(plazaId, message, cancellationToken);
        _legacyPlazaUiBridge.PlayAlertRing();
        return new ApiResult(ApiCode.OK, "OK");
    }

    public Task<ApiResult> ShowMessageAsync(string plazaId, LaneMessageDto message, CancellationToken cancellationToken = default)
        => _legacyPlazaUiBridge.ShowMessageAsync(plazaId, message, cancellationToken);

    public async Task<ApiResult> ShowOverloadAlarmAsync(string plazaId, OverloadWarningDto warning, bool playSpeech, CancellationToken cancellationToken = default)
    {
        await _notificationApplicationService.ShowOverloadAlarmAsync(plazaId, warning, playSpeech, cancellationToken);
        return await _legacyPlazaUiBridge.ShowOverloadAlarmAsync(plazaId, warning, playSpeech, cancellationToken);
    }

    public async Task<ApiResult> ShowLaneSpecialAsync(string plazaId, LaneSpecialDto message, CancellationToken cancellationToken = default)
    {
        await _notificationApplicationService.ShowLaneSpecialAsync(plazaId, message, cancellationToken);
        return await _legacyPlazaUiBridge.ShowLaneSpecialAsync(plazaId, message, cancellationToken);
    }

    public Task<ApiResult> ShowBulkTransAsync(string plazaId, BulkTransportDto dto, CancellationToken cancellationToken = default)
        => _legacyPlazaUiBridge.ShowBulkTransportAsync(plazaId, dto, cancellationToken);

    public Task<ApiResult> ShowBillInfoAsync(string plazaId, BillInfoRequestDto dto, CancellationToken cancellationToken = default)
        => _legacyPlazaUiBridge.ShowBillInfoAsync(plazaId, dto, cancellationToken);

    public Task<ApiResult> ShowConfirmEnInfoAsync(string plazaId, ConfirmEnInfoDto dto, CancellationToken cancellationToken = default)
        => _legacyPlazaUiBridge.ShowConfirmEnInfoAsync(plazaId, dto, cancellationToken);

    public Task<TrafficEventPushResponse> EnqueueTrafficEventAsync(TrafficEventPushRequestDto request, CancellationToken cancellationToken = default)
        => _trafficEventApplicationService.SubmitAsync(request, cancellationToken);
}