using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;
using Uixe.Watcher.Ring;
using Uixe.Watcher.TCO;

namespace Uixe.Watcher.Services
{
    public sealed class LaneApplicationService : ILaneApplicationService
    {
        private readonly ILogger<LaneApplicationService> _logger;
        private readonly IMemoryCache _cache;
        private readonly AppSettings _settings;
        private readonly TrafficEventQueueService _trafficEventQueue;
        private readonly IPlazaContextService _plazaContextService;
        private readonly ILegacyLaneInteractionService _legacyLaneInteractionService;
        private readonly ITrafficEventApplicationService _trafficEventApplicationService;

        public LaneApplicationService(
            ILogger<LaneApplicationService> logger,
            IMemoryCache cache,
            IOptions<AppSettings> options,
            TrafficEventQueueService trafficEventQueue,
            IPlazaContextService plazaContextService,
            ILegacyLaneInteractionService legacyLaneInteractionService,
            ITrafficEventApplicationService trafficEventApplicationService)
        {
            _logger = logger;
            _cache = cache;
            _settings = options.Value;
            _trafficEventQueue = trafficEventQueue;
            _plazaContextService = plazaContextService;
            _legacyLaneInteractionService = legacyLaneInteractionService;
            _trafficEventApplicationService = trafficEventApplicationService;
        }

        public Task<Uixe.Copilot.Contracts.Responses.ApiResult> ShowLaneStatusAsync(string plazaId, string laneNo, object status, CancellationToken cancellationToken = default)
            => ExecuteOnPlazaAsync(plazaId, frm => frm.ShowLaneInfor(plazaId, laneNo, (LaneStatus)status));

        public async Task<Uixe.Copilot.Contracts.Responses.ApiResult> ShowWeightMessageAsync(string plazaId, object message, CancellationToken cancellationToken = default)
        {
            var frm = GetPlazaForm(plazaId);
            if (frm == null)
            {
                return new Uixe.Copilot.Contracts.Responses.ApiResult(Uixe.Copilot.Contracts.Responses.ApiCode.NotFound, $"没有找到{plazaId}的收费站ID");
            }

            await Task.Run(() =>
            {
                frm.Invoke(() =>
                {
                    var tco = _cache.GetOrCreate($"{nameof(frmWeightTCOCall)}_{plazaId}", _ =>
                    {
                        var wtco = new frmWeightTCOCall(frm.GetPlaza(plazaId), frm._runtimeSetting, frm.settings, _logger);
                        wtco.LoadInfo();
                        wtco.Hide();
                        return wtco;
                    });
                    tco.ShowTCOMsg((MsgWeightTCOCALL)message);
                });
                Task.Run(PlayUitls.PlayRing);
            }, cancellationToken);

            return new Uixe.Copilot.Contracts.Responses.ApiResult(Uixe.Copilot.Contracts.Responses.ApiCode.OK, "OK");
        }

        public async Task<Uixe.Copilot.Contracts.Responses.ApiResult> ShowTcoConfirmAsync(string plazaId, object message, CancellationToken cancellationToken = default)
        {
            var frm = GetPlazaForm(plazaId);
            if (frm == null)
            {
                return new Uixe.Copilot.Contracts.Responses.ApiResult(Uixe.Copilot.Contracts.Responses.ApiCode.NotFound, $"没有找到{plazaId}的收费站ID");
            }

            await Task.Run(() =>
            {
                frm.Invoke(() =>
                {
                    var tco = _cache.GetOrCreate<frmShowTCOCall>($"{nameof(frmShowTCOCall)}_{plazaId}", _ => new frmShowTCOCall(frm, frm.GetPlaza(plazaId)));
                    tco.TCOCallxxx = (TCOCall)message;
                    tco.Show();
                    Task.Run(PlayUitls.PlayRing);
                });
            }, cancellationToken);

            return new Uixe.Copilot.Contracts.Responses.ApiResult(Uixe.Copilot.Contracts.Responses.ApiCode.OK, "OK");
        }

        public Task<Uixe.Copilot.Contracts.Responses.ApiResult> ShowMessageAsync(string plazaId, object message, CancellationToken cancellationToken = default)
            => ExecuteOnPlazaAsync(plazaId, frm => frm.ShowMessageView((MsgInfo)message));

        public async Task<Uixe.Copilot.Contracts.Responses.ApiResult> ShowOverloadAlarmAsync(string plazaId, string title, string context, bool playSpeech, CancellationToken cancellationToken = default)
        {
            var frm = GetPlazaForm(plazaId);
            if (frm == null)
            {
                return new Uixe.Copilot.Contracts.Responses.ApiResult(Uixe.Copilot.Contracts.Responses.ApiCode.NotFound, $"没有找到{plazaId}的收费站ID");
            }

            await Task.Run(() =>
            {
                frm.Invoke(() => frm.Alert(title, context));
                if (playSpeech && frm.settings?.laneVideoMute == false)
                {
                    SpeechUtils.Speecher.SpeakAsync(title);
                }
            }, cancellationToken);

            return new Uixe.Copilot.Contracts.Responses.ApiResult(Uixe.Copilot.Contracts.Responses.ApiCode.OK, "OK");
        }

        public async Task<Uixe.Copilot.Contracts.Responses.ApiResult> ShowLaneSpecialAsync(string plazaId, object message, CancellationToken cancellationToken = default)
        {
            var frm = GetPlazaForm(plazaId);
            if (frm == null)
            {
                return new Uixe.Copilot.Contracts.Responses.ApiResult(Uixe.Copilot.Contracts.Responses.ApiCode.NotFound, $"没有找到{plazaId}的收费站ID");
            }

            var msg = (Lanespecial)message;
            await Task.Run(() =>
            {
                var canShow = frm.settings?.Only6769 != true || msg.SubHead.StaffID == "777777" || msg.SubHead.StaffID == "999999";
                if (!canShow || frm.settings?.Lanespecial != true)
                {
                    return;
                }

                string laneid = $"650{msg.Head.NetNo}{msg.Head.PlazaNo}{msg.Head.LaneID}";
                if (_cache.TryGetValue(laneid, out Lanespecial _))
                {
                    return;
                }

                _cache.Set(laneid, msg, TimeSpan.FromSeconds(3));
                frm.Invoke(() => frm.Alert(msg.Title, msg.Context));
                if (frm.settings?.laneVideoMute == false)
                {
                    if (int.Parse(msg.MsgT_Lanespecial_Waste.SPECIAL_TYPE) == 175)
                    {
                        SpeechUtils.Speecher.SpeakAsync(msg.Context);
                    }
                    else
                    {
                        SpeechUtils.Speecher.SpeakAsync(msg.Title);
                    }
                }
            }, cancellationToken);

            return new Uixe.Copilot.Contracts.Responses.ApiResult(Uixe.Copilot.Contracts.Responses.ApiCode.OK, "OK");
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

            var plazas = _plazaContextService.GetPlazas();
            if (request == null || string.IsNullOrWhiteSpace(request.LaneNo) || plazas.Count == 0)
            {
                return false;
            }

            foreach (var currentPlaza in plazas)
            {
                var currentLane = currentPlaza?.Lanes?.FirstOrDefault(item => IsLaneMatch(item?.LaneNo, item?.LaneId, request.LaneNo));
                if (currentLane == null)
                {
                    continue;
                }

                var currentForm = GetPlazaForm(currentPlaza.Id);
                if (currentForm == null)
                {
                    continue;
                }

                handler = new PlazaTrafficEventDisplayHandler(currentForm);
                plaza = new T_Plaza { Id = currentPlaza.Id, StationId = currentPlaza.StationId, StationName = currentPlaza.StationName, Lanes = currentPlaza.Lanes?.Select(x => new T_Lane { Id = x.Id, LaneId = x.LaneId, LaneNo = x.LaneNo }).ToList() };
                lane = new T_Lane { Id = currentLane.Id, LaneId = currentLane.LaneId, LaneNo = currentLane.LaneNo };
                formRequest = MapRequest(request);
                return true;
            }

            return false;
        }

        private frmPlaza GetPlazaForm(string plazaId)
        {
            return _plazaContextService.GetPlazaHost(plazaId) as frmPlaza ?? _cache.Get<frmPlaza>($"{nameof(frmPlaza)}_{plazaId}");
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

        private Task<Uixe.Copilot.Contracts.Responses.ApiResult> ExecuteOnPlazaAsync(string plazaId, Action<frmPlaza> action)
        {
            var frm = GetPlazaForm(plazaId);
            if (frm == null)
            {
                return Task.FromResult(new Uixe.Copilot.Contracts.Responses.ApiResult(Uixe.Copilot.Contracts.Responses.ApiCode.NotFound, $"没有找到{plazaId}的收费站ID"));
            }

            return Task.Run(() =>
            {
                action(frm);
                return new Uixe.Copilot.Contracts.Responses.ApiResult(Uixe.Copilot.Contracts.Responses.ApiCode.OK, "OK");
            });
        }

        private static bool IsLaneMatch(string laneNoValue, string laneIdValue, string laneNo)
        {
            if (string.IsNullOrWhiteSpace(laneNo))
            {
                return false;
            }

            return IsLaneTokenMatch(laneNo, laneNoValue)
                || IsLaneTokenMatch(laneNo, laneIdValue);
        }

        private static bool IsLaneTokenMatch(string left, string right)
        {
            var leftValue = NormalizeLaneToken(left);
            var rightValue = NormalizeLaneToken(right);
            if (string.IsNullOrEmpty(leftValue) || string.IsNullOrEmpty(rightValue))
            {
                return false;
            }

            return leftValue == rightValue || leftValue.EndsWith(rightValue) || rightValue.EndsWith(leftValue);
        }

        private static string NormalizeLaneToken(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            return new string(value.Where(c => !char.IsWhiteSpace(c) && c != '-' && c != '_').ToArray()).ToUpperInvariant();
        }

        private static TrafficEventPushRequest MapRequest(TrafficEventPushRequestDto request)
        {
            return new TrafficEventPushRequest
            {
                RecordId = request.RecordId,
                EventType = request.EventType,
                LaneNo = request.LaneNo,
                CapTime = request.CapTime,
                StartTime = request.StartTime,
                Period = request.Period,
                PeriodByMili = request.PeriodByMili,
                MaxQueueLen = request.MaxQueueLen,
                ImageList = request.ImageList,
                VideoList = request.VideoList
            };
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

                                FormClosedEventHandler closedHandler = null;
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
