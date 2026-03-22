using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;
using Uixe.Watcher.TCO;

namespace Uixe.Watcher.Services
{
    public sealed class LaneApplicationService : ILaneApplicationService
    {
        private readonly ILogger<LaneApplicationService> _logger;
        private readonly AppSettings _settings;
        private readonly TrafficEventQueueService _trafficEventQueue;
        private readonly IPlazaContextService _plazaContextService;
        private readonly ILegacyPlazaUiBridge _legacyPlazaUiBridge;
        private readonly ITrafficEventApplicationService _trafficEventApplicationService;
        private readonly INotificationApplicationService _notificationApplicationService;
        private readonly ILegacyWindowCoordinator _legacyWindowCoordinator;
        private readonly ITcoWindowApplicationService _tcoWindowApplicationService;
        private readonly ILegacyTcoInteractionService _legacyTcoInteractionService;

        public LaneApplicationService(
            ILogger<LaneApplicationService> logger,
            IOptions<AppSettings> options,
            TrafficEventQueueService trafficEventQueue,
            IPlazaContextService plazaContextService,
            ILegacyPlazaUiBridge legacyPlazaUiBridge,
            ITrafficEventApplicationService trafficEventApplicationService,
            INotificationApplicationService notificationApplicationService,
            ILegacyWindowCoordinator legacyWindowCoordinator,
            ITcoWindowApplicationService tcoWindowApplicationService,
            ILegacyTcoInteractionService legacyTcoInteractionService)
        {
            _logger = logger;
            _settings = options.Value;
            _trafficEventQueue = trafficEventQueue;
            _plazaContextService = plazaContextService;
            _legacyPlazaUiBridge = legacyPlazaUiBridge;
            _trafficEventApplicationService = trafficEventApplicationService;
            _notificationApplicationService = notificationApplicationService;
            _legacyWindowCoordinator = legacyWindowCoordinator;
            _tcoWindowApplicationService = tcoWindowApplicationService;
            _legacyTcoInteractionService = legacyTcoInteractionService;
        }

        public Task<Uixe.Copilot.Contracts.Responses.ApiResult> ShowLaneStatusAsync(string plazaId, string laneNo, object status, CancellationToken cancellationToken = default)
            => _legacyPlazaUiBridge.ShowLaneStatusAsync(plazaId, laneNo, status, cancellationToken);

        public Task<Uixe.Copilot.Contracts.Responses.ApiResult> ShowLaneLostAsync(string plazaId, string laneNo, CancellationToken cancellationToken = default)
            => _legacyPlazaUiBridge.ShowLaneLostAsync(plazaId, laneNo, cancellationToken);

        public async Task<Uixe.Copilot.Contracts.Responses.ApiResult> ShowWeightMessageAsync(string plazaId, object message, CancellationToken cancellationToken = default)
        {
            await _notificationApplicationService.ShowWeightMessageAsync(plazaId, message, cancellationToken);
            await _tcoWindowApplicationService.ShowWeightMessageAsync(plazaId, message, cancellationToken);
            await _legacyTcoInteractionService.ShowWeightMessageAsync(plazaId, message, cancellationToken);
            _legacyPlazaUiBridge.PlayAlertRing();
            return new Uixe.Copilot.Contracts.Responses.ApiResult(Uixe.Copilot.Contracts.Responses.ApiCode.OK, "OK");
        }

        public async Task<Uixe.Copilot.Contracts.Responses.ApiResult> ShowTcoConfirmAsync(string plazaId, object message, CancellationToken cancellationToken = default)
        {
            await _notificationApplicationService.ShowTcoConfirmAsync(plazaId, message, cancellationToken);
            await _tcoWindowApplicationService.ShowTcoConfirmAsync(plazaId, message, cancellationToken);
            await _legacyTcoInteractionService.ShowTcoConfirmAsync(plazaId, message, cancellationToken);
            _legacyPlazaUiBridge.PlayAlertRing();
            return new Uixe.Copilot.Contracts.Responses.ApiResult(Uixe.Copilot.Contracts.Responses.ApiCode.OK, "OK");
        }

        public Task<Uixe.Copilot.Contracts.Responses.ApiResult> ShowMessageAsync(string plazaId, object message, CancellationToken cancellationToken = default)
            => _legacyPlazaUiBridge.ShowMessageAsync(plazaId, message, cancellationToken);

        public async Task<Uixe.Copilot.Contracts.Responses.ApiResult> ShowOverloadAlarmAsync(string plazaId, string title, string context, bool playSpeech, CancellationToken cancellationToken = default)
        {
            await _notificationApplicationService.ShowOverloadAlarmAsync(plazaId, title, context, playSpeech, cancellationToken);
            return await _legacyPlazaUiBridge.ShowOverloadAlarmAsync(plazaId, title, context, playSpeech, cancellationToken);
        }

        public async Task<Uixe.Copilot.Contracts.Responses.ApiResult> ShowLaneSpecialAsync(string plazaId, object message, CancellationToken cancellationToken = default)
        {
            await _notificationApplicationService.ShowLaneSpecialAsync(plazaId, message, cancellationToken);
            return await _legacyPlazaUiBridge.ShowLaneSpecialAsync(plazaId, message, cancellationToken);
        }

        public Task<Uixe.Copilot.Contracts.Responses.ApiResult> ShowBulkTransAsync(string plazaId, object dto, CancellationToken cancellationToken = default)
            => _legacyPlazaUiBridge.ShowBulkTransportAsync(plazaId, ((BulklyDto)dto).ToBulkTransportDto(), cancellationToken);

        public Task<Uixe.Copilot.Contracts.Responses.ApiResult> ShowBillInfoAsync(string plazaId, object dto, CancellationToken cancellationToken = default)
            => _legacyPlazaUiBridge.ShowBillInfoAsync(plazaId, ((BillInfoDto)dto).ToBillInfoRequestDto(), cancellationToken);

        public Task<Uixe.Copilot.Contracts.Responses.ApiResult> ShowConfirmEnInfoAsync(string plazaId, object dto, CancellationToken cancellationToken = default)
            => _legacyPlazaUiBridge.ShowConfirmEnInfoAsync(plazaId, ((ConfirmEnInfo)dto).ToConfirmEnInfoDto(), cancellationToken);

        public Task<Uixe.Copilot.Contracts.Responses.TrafficEventPushResponse> EnqueueTrafficEventAsync(TrafficEventPushRequestDto request, CancellationToken cancellationToken = default)
            => _trafficEventApplicationService.SubmitAsync(request, cancellationToken);

        private bool TryResolveTrafficEventTarget(TrafficEventPushRequestDto request, out ITrafficEventDisplayHandler handler, out T_Plaza plaza, out T_Lane lane, out TrafficEventPushRequest formRequest)
        {
            handler = null;
            plaza = null;
            lane = null;
            formRequest = null;

            var resolved = _legacyPlazaUiBridge.TryResolveTrafficEventTarget(request);
            if (resolved == null)
            {
                return false;
            }

            if (resolved.DisplayAction is null
                || resolved.Plaza is not T_Plaza resolvedPlaza
                || resolved.Lane is not T_Lane resolvedLane
                || resolved.FormRequest is not TrafficEventPushRequest resolvedRequest)
            {
                return false;
            }

            handler = new PlazaTrafficEventDisplayHandler(resolved.DisplayAction);
            plaza = resolvedPlaza;
            lane = resolvedLane;
            formRequest = resolvedRequest;
            return true;
        }

        private static Uixe.Copilot.Contracts.Responses.TrafficEventPushResponse CreateTrafficEventResponse(int code, string message)
        {
            return new Uixe.Copilot.Contracts.Responses.TrafficEventPushResponse
            {
                Code = code,
                Message = message,
                Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds()
            };
        }

        private sealed class PlazaTrafficEventDisplayHandler : ITrafficEventDisplayHandler
        {
            private readonly Func<CancellationToken, Task> _displayAction;

            public PlazaTrafficEventDisplayHandler(Func<CancellationToken, Task> displayAction)
            {
                _displayAction = displayAction;
            }

            public Task ShowAsync(T_Plaza plaza, T_Lane lane, TrafficEventPushRequest request, CancellationToken cancellationToken = default)
                => _displayAction(cancellationToken);
        }
    }
}
