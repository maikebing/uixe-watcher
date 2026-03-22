using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Contracts.Responses;
namespace Uixe.Copilot.Application.Services;

public sealed class LaneApplicationService : ILaneApplicationService
{
    private readonly IPlazaContextService _plazaContextService;
    private readonly ILaneStatusSnapshotStore _laneStatusSnapshotStore;
    private readonly ILegacyPlazaUiBridge _legacyPlazaUiBridge;
    private readonly ITrafficEventApplicationService _trafficEventApplicationService;
    private readonly INotificationApplicationService _notificationApplicationService;
    private readonly ITcoWindowApplicationService _tcoWindowApplicationService;
    private readonly ILegacyTcoInteractionService _legacyTcoInteractionService;

    public LaneApplicationService(
        IPlazaContextService plazaContextService,
        ILaneStatusSnapshotStore laneStatusSnapshotStore,
        ILegacyPlazaUiBridge legacyPlazaUiBridge,
        ITrafficEventApplicationService trafficEventApplicationService,
        INotificationApplicationService notificationApplicationService,
        ITcoWindowApplicationService tcoWindowApplicationService,
        ILegacyTcoInteractionService legacyTcoInteractionService)
    {
        _plazaContextService = plazaContextService;
        _laneStatusSnapshotStore = laneStatusSnapshotStore;
        _legacyPlazaUiBridge = legacyPlazaUiBridge;
        _trafficEventApplicationService = trafficEventApplicationService;
        _notificationApplicationService = notificationApplicationService;
        _tcoWindowApplicationService = tcoWindowApplicationService;
        _legacyTcoInteractionService = legacyTcoInteractionService;
    }

    public Task<ApiResult> ShowLaneStatusAsync(string plazaId, string laneNo, LaneStatusDto status, CancellationToken cancellationToken = default)
    {
        var plaza = _plazaContextService.GetPlazas().FirstOrDefault(item => string.Equals(item.Id, plazaId, StringComparison.OrdinalIgnoreCase));
        var lane = plaza?.Lanes.FirstOrDefault(item => string.Equals(item.LaneNo, laneNo, StringComparison.OrdinalIgnoreCase));
        _laneStatusSnapshotStore.Upsert(plazaId, laneNo, status, plaza, lane);
        return _legacyPlazaUiBridge.ShowLaneStatusAsync(plazaId, laneNo, status, cancellationToken);
    }

    public Task<ApiResult> ShowLaneLostAsync(string plazaId, string laneNo, CancellationToken cancellationToken = default)
    {
        var plaza = _plazaContextService.GetPlazas().FirstOrDefault(item => string.Equals(item.Id, plazaId, StringComparison.OrdinalIgnoreCase));
        var lane = plaza?.Lanes.FirstOrDefault(item => string.Equals(item.LaneNo, laneNo, StringComparison.OrdinalIgnoreCase));
        _laneStatusSnapshotStore.MarkLaneLost(plazaId, laneNo, plaza, lane);
        return _legacyPlazaUiBridge.ShowLaneLostAsync(plazaId, laneNo, cancellationToken);
    }

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
    {
        var plaza = _plazaContextService.GetPlazas().FirstOrDefault(item => string.Equals(item.Id, plazaId, StringComparison.OrdinalIgnoreCase));
        var lane = plaza?.Lanes.FirstOrDefault(item => string.Equals(item.LaneNo, message.LaneNo, StringComparison.OrdinalIgnoreCase));
        _laneStatusSnapshotStore.AddMessage(plazaId, message, plaza, lane);
        return _legacyPlazaUiBridge.ShowMessageAsync(plazaId, message, cancellationToken);
    }

    public async Task<ApiResult> ShowOverloadAlarmAsync(string plazaId, OverloadWarningDto warning, bool playSpeech, CancellationToken cancellationToken = default)
    {
        var plaza = _plazaContextService.GetPlazas().FirstOrDefault(item => string.Equals(item.Id, plazaId, StringComparison.OrdinalIgnoreCase));
        _laneStatusSnapshotStore.AddOverloadAlert(plazaId, warning, plaza, null);
        await _notificationApplicationService.ShowOverloadAlarmAsync(plazaId, warning, playSpeech, cancellationToken);
        return await _legacyPlazaUiBridge.ShowOverloadAlarmAsync(plazaId, warning, playSpeech, cancellationToken);
    }

    public async Task<ApiResult> ShowLaneSpecialAsync(string plazaId, LaneSpecialDto message, CancellationToken cancellationToken = default)
    {
        var plaza = _plazaContextService.GetPlazas().FirstOrDefault(item => string.Equals(item.Id, plazaId, StringComparison.OrdinalIgnoreCase));
        var lane = plaza?.Lanes.FirstOrDefault(item => string.Equals(item.LaneId, message.LaneId, StringComparison.OrdinalIgnoreCase));
        _laneStatusSnapshotStore.AddLaneSpecial(plazaId, message, plaza, lane);
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