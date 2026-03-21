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
        private readonly ILegacyLaneInteractionService _legacyLaneInteractionService;
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
            ILegacyLaneInteractionService legacyLaneInteractionService,
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
            _legacyLaneInteractionService = legacyLaneInteractionService;
            _trafficEventApplicationService = trafficEventApplicationService;
            _notificationApplicationService = notificationApplicationService;
            _legacyWindowCoordinator = legacyWindowCoordinator;
            _tcoWindowApplicationService = tcoWindowApplicationService;
            _legacyTcoInteractionService = legacyTcoInteractionService;
        }

        public Task<Uixe.Copilot.Contracts.Responses.ApiResult> ShowLaneStatusAsync(string plazaId, string laneNo, object status, CancellationToken cancellationToken = default)
            => _legacyPlazaUiBridge.ShowLaneStatusAsync(plazaId, laneNo, status, cancellationToken);

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
            => ExecuteLegacyInteractionAsync(plazaId, dto, static data =>
            {
                string laneId = $"650{data.Head?.NetNo}{data.Head?.PlazaNo}{data.Head?.LaneId}";
                return laneId;
            }, (service, host, laneId, data, ct) => service.ShowBulkTransportAsync(host, laneId, data, ct), ((BulklyDto)dto).ToBulkTransportDto(), cancellationToken);

        public Task<Uixe.Copilot.Contracts.Responses.ApiResult> ShowBillInfoAsync(string plazaId, object dto, CancellationToken cancellationToken = default)
            => ExecuteLegacyInteractionAsync(plazaId, dto, static data =>
            {
                string laneId = $"650{data.Head?.NetNo}{data.Head?.PlazaNo}{data.Head?.LaneId}";
                return laneId;
            }, (service, host, laneId, data, ct) => service.ShowBillInfoAsync(host, laneId, data, ct), ((BillInfoDto)dto).ToBillInfoRequestDto(), cancellationToken);

        public Task<Uixe.Copilot.Contracts.Responses.ApiResult> ShowConfirmEnInfoAsync(string plazaId, object dto, CancellationToken cancellationToken = default)
            => ExecuteLegacyInteractionAsync(plazaId, dto, static data => data.PlazaId ?? string.Empty, (service, host, _, data, ct) => service.ShowConfirmEnInfoAsync(host, data, ct), ((ConfirmEnInfo)dto).ToConfirmEnInfoDto(), cancellationToken);

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

            if (resolved.DisplayHost is not frmPlaza currentForm
                || resolved.Plaza is not T_Plaza resolvedPlaza
                || resolved.Lane is not T_Lane resolvedLane
                || resolved.FormRequest is not TrafficEventPushRequest resolvedRequest)
            {
                return false;
            }

            handler = new PlazaTrafficEventDisplayHandler(currentForm);
            plaza = resolvedPlaza;
            lane = resolvedLane;
            formRequest = resolvedRequest;
            return true;
        }

        private async Task<Uixe.Copilot.Contracts.Responses.ApiResult> ExecuteLegacyInteractionAsync<TLegacy>(
            string plazaId,
            object originalDto,
            Func<TLegacy, string> laneIdFactory,
            Func<ILegacyLaneInteractionService, object, string, TLegacy, CancellationToken, Task<bool>> executor,
            TLegacy mappedDto,
            CancellationToken cancellationToken)
        {
            var host = _plazaContextService.GetPlazaHost(plazaId);
            if (host == null)
            {
                return new Uixe.Copilot.Contracts.Responses.ApiResult(Uixe.Copilot.Contracts.Responses.ApiCode.NotFound, $"没有找到{plazaId}的收费站ID");
            }

            var laneId = laneIdFactory(mappedDto);
            var success = await executor(_legacyLaneInteractionService, host, laneId, mappedDto, cancellationToken);
            return success
                ? new Uixe.Copilot.Contracts.Responses.ApiResult(Uixe.Copilot.Contracts.Responses.ApiCode.OK, "OK")
                : new Uixe.Copilot.Contracts.Responses.ApiResult(Uixe.Copilot.Contracts.Responses.ApiCode.BadRequest, $"兼容处理失败: {originalDto.GetType().Name}");
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
            private readonly frmPlaza _form;

            public PlazaTrafficEventDisplayHandler(frmPlaza form)
            {
                _form = form;
            }

            public Task ShowAsync(T_Plaza plaza, T_Lane lane, TrafficEventPushRequest request, CancellationToken cancellationToken = default)
            {
                TaskCompletionSource<bool> taskCompletionSource = new(TaskCreationOptions.RunContinuationsAsynchronously);

                try
                {
                    if (_form.IsDisposed || !_form.IsHandleCreated)
                    {
                        taskCompletionSource.SetResult(true);
                        return taskCompletionSource.Task;
                    }

                    _form.BeginInvoke((MethodInvoker)delegate
                    {
                        try
                        {
                            if (!_form.IsDisposed && _form.IsHandleCreated)
                            {
                                var trafficForm = _form.ShowTrafficEvent(plaza, lane, request);
                                if (trafficForm == null || trafficForm.IsDisposed)
                                {
                                    taskCompletionSource.TrySetResult(true);
                                    return;
                                }

                                FormClosedEventHandler? closedHandler = null;
                                closedHandler = delegate
                                {
                                    trafficForm.FormClosed -= closedHandler;
                                    taskCompletionSource.TrySetResult(true);
                                };
                                trafficForm.FormClosed += closedHandler;
                                return;
                            }

                            taskCompletionSource.TrySetResult(true);
                        }
                        catch (Exception ex)
                        {
                            taskCompletionSource.TrySetException(ex);
                        }
                    });
                }
                catch (Exception ex)
                {
                    taskCompletionSource.TrySetException(ex);
                }

                return taskCompletionSource.Task;
            }
        }
    }
}
