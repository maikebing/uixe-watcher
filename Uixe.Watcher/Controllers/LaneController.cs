using DevExpress.Utils.Drawing.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;
using Uixe.Watcher.Ring;
using Uixe.Watcher.Services;
using Uixe.Watcher.TCO;
using static DevExpress.Xpo.DB.DataStoreLongrunnersWatch;
namespace Uixe.Watcher.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public partial class LaneController : ControllerBase
    {

        private readonly ILogger<LaneController> _logger;
        private readonly AppSettings option;
        private readonly IMemoryCache _cache;
        private readonly TrafficEventQueueService _trafficEventQueue;

        public LaneController(ILogger<LaneController> logger, IOptions<AppSettings> option, IMemoryCache cache, TrafficEventQueueService trafficEventQueue)
        {
            _logger = logger;
            this.option = option.Value;
            _cache = cache;
            _trafficEventQueue = trafficEventQueue;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult>> emrc_main_status(string plazaid, string laneno, LaneStatus status)
        {
            try
            {
                await Task.Run(() =>
                 {
                     var frm = _cache.Get<frmPlaza>($"{nameof(frmPlaza)}_{plazaid}");
                     frm?.ShowLaneInfor(plazaid, laneno, status);
                 });
                return Ok(new ApiResult(ApiCode.OK, "OK"));
            }
            catch (Exception)
            {

                return BadRequest(new ApiResult(ApiCode.BadRequst, "OK"));
            }

        }

        [HttpPost]
        public async Task<ActionResult<ApiResult>> emrc_main_tco_weightAsync(string plazaid, MsgWeightTCOCALL msgWeight)
        {
            try
            {
                await Task.Run(() =>
                {
                    try
                    {
                        var frm = _cache.Get<frmPlaza>($"{nameof(frmPlaza)}_{plazaid}");
                        if (frm != null)
                        {
                            frm.Invoke(() =>
                            {
                                var tco = _cache.GetOrCreate($"{nameof(frmWeightTCOCall)}_{plazaid}", c =>
                                {
                                    var wtco= new frmWeightTCOCall(frm.GetPlaza(plazaid), frm._runtimeSetting, frm.settings, _logger);
                                    wtco.LoadInfo();
                                    wtco.Hide();
                                    return wtco;
                                });
                                tco.ShowTCOMsg(msgWeight);

                            });
                            Task.Run(PlayUitls.PlayRing);
                        }
                        else
                        {
                            _logger.LogWarning($"没有找到{plazaid}的收费站ID");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"无法打开{nameof(frmPlaza)} 站代码为{plazaid}");
                      
                    }
                    
                });
                return Ok(new ApiResult(ApiCode.OK, "OK"));
            }
            catch (Exception ex)
            {

                return BadRequest(new ApiResult(ApiCode.BadRequst, ex.Message));
            }

        }

        [HttpPost]
        public async Task<ActionResult<ApiResult>> emrc_main_tco_confirmAsync(string plazaid, TCOCall msg)
        {
            try
            {
                await Task.Run(() =>
                {
                    var frm = _cache.Get<frmPlaza>($"{nameof(frmPlaza)}_{plazaid}");
                    if (frm != null)
                    {
                        frm.Invoke(() =>
                        {
                            var tco = _cache.GetOrCreate<frmShowTCOCall>($"{nameof(frmShowTCOCall)}_{plazaid}", c => new frmShowTCOCall(frm, frm.GetPlaza(plazaid)));
                            tco.TCOCallxxx = msg;
                            tco.Show();
                            Task.Run(PlayUitls.PlayRing);
                        });
                    }
                    else
                    {
                        _logger.LogWarning($"没有找到{plazaid}的收费站ID");
                    }
                });
                return Ok(new ApiResult(ApiCode.OK, "OK"));
            }
            catch (Exception)
            {

                return BadRequest(new ApiResult(ApiCode.BadRequst, "OK"));
            }
        }


        [HttpPost]
        public async Task<ActionResult<ApiResult>> emrc_main_message(string plazaid, MsgInfo msg)
        {
            try
            {

                await Task.Run(() =>
                {
                    var frm = _cache.Get<frmPlaza>($"{nameof(frmPlaza)}_{plazaid}");
                    frm?.ShowMessageView(msg);
                });
                return Ok(new ApiResult(ApiCode.OK, "OK"));
            }
            catch (Exception)
            {

                return BadRequest(new ApiResult(ApiCode.BadRequst, "OK"));
            }
        }
        [HttpPost]
        public async Task<ActionResult<ApiResult>> OverloadAlarm(string plazaid, OverloadWarning warn)
        {
            try
            {
                await Task.Run(() =>
                {
                    var frm = _cache.Get<frmPlaza>($"{nameof(frmPlaza)}_{plazaid}");
                    frm?.Invoke(() => frm.Alert(warn.Title, warn.Context));
                    if (frm?.settings?.laneVideoMute == false)
                    {
                        SpeechUtils.Speecher.SpeakAsync(warn.Title);
                    }
                });
                return Ok(new ApiResult(ApiCode.OK, "OK"));
            }
            catch (Exception)
            {

                return BadRequest(new ApiResult(ApiCode.BadRequst, "OK"));
            }
        }
        [HttpPost]
        public async Task<ActionResult<ApiResult>> Lanespecial(string plazaid, Lanespecial msg)
        {
            try
            {
                await Task.Run(() =>
                {
                    var frm = _cache.Get<frmPlaza>($"{nameof(frmPlaza)}_{plazaid}");
                    bool canshow = true;
                    if (frm?.settings?.Only6769==true)
                    {
                        if (msg.SubHead.StaffID == "777777" || msg.SubHead.StaffID == "999999")
                        {
                            canshow = true;
                        }
                        else
                        {
                            canshow=false;
                        }
                    }
                    else
                    {
                        canshow = true;
                    }
                    if (canshow)
                    {
                        if (frm?.settings?.Lanespecial == true)
                        {
                            string laneid = $"650{msg.Head.NetNo}{msg.Head.PlazaNo}{msg.Head.LaneID}";
                            Lanespecial msgold = null;
                            if (!_cache.TryGetValue(laneid, out msgold))
                            {
                                var msg1 = _cache.Set(laneid, msg, TimeSpan.FromSeconds(3));

                                frm?.Invoke(() => frm.Alert(msg.Title, msg.Context));
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
                            }
                        }
                    }
                });
                return Ok(new ApiResult(ApiCode.OK, "OK"));
            }
            catch (Exception)
            {

                return BadRequest(new ApiResult(ApiCode.BadRequst, "OK"));
            }
        }



        [HttpPost]
        public async Task<ActionResult<ApiResult>> Bulktrans(string plazaid, BulklyDto dto)
        {

            try
            {
                await Task.Run(() =>
                  {
                      var frm = _cache.Get<frmPlaza>($"{nameof(frmPlaza)}_{plazaid}");
                      frm?.Invoke(() =>
                    {
                             string laneid = $"650{dto.Head.NetNo}{dto.Head.PlazaNo}{dto.Head.LaneID}";
                             frm.ShowBulktrans(laneid, dto);
                         });
                  });
                return Ok(new ApiResult(ApiCode.OK, "OK"));

            }
            catch (Exception)
            {
                return BadRequest(new ApiResult(ApiCode.BadRequst, "OK"));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult>> bill_info(string plazaid, BillInfoDto dto)
        {

            try
            {
                await Task.Run(() =>
                {
                    var frm = _cache.Get<frmPlaza>($"{nameof(frmPlaza)}_{plazaid}");
                    frm?.Invoke(() =>
                    {
                        string laneid = $"650{dto.Head.NetNo}{dto.Head.PlazaNo}{dto.Head.LaneID}";
                        frm.ShowBillInfo(laneid, dto);
                    });
                });
                return Ok(new ApiResult(ApiCode.OK, "OK"));

            }
            catch (Exception)
            {
                return BadRequest(new ApiResult(ApiCode.BadRequst, "OK"));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult>> ConfirmEnInfo(string plazaId, ConfirmEnInfo dto)
        {

            try
            {
                await Task.Run(() =>
                {
                    var frm = _cache.Get<frmPlaza>($"{nameof(frmPlaza)}_{plazaId}");
                    frm?.Invoke(() =>
                    {
                        frm.ShowConfirmEnInfo( dto);
                    });
                });
                return Ok(new ApiResult(ApiCode.OK, "OK"));

            }
            catch (Exception)
            {
                return BadRequest(new ApiResult(ApiCode.BadRequst, "OK"));
            }
        }

        [HttpPost]
        public ActionResult<TrafficEventPushResponse> TrafficEvent([FromBody] TrafficEventPushRequest request)
        {
            if (request == null)
            {
                return BadRequest(CreateTrafficEventResponse(1, "请求体不能为空"));
            }

            if (string.IsNullOrWhiteSpace(request.LaneNo))
            {
                return BadRequest(CreateTrafficEventResponse(1, "LaneNo不能为空"));
            }

            try
            {
                if (!TryResolveTrafficEventTarget(request, out var frm, out var plaza, out var lane))
                {
                    _logger.LogWarning("交通事件未匹配到车道，LaneNo={LaneNo}, RecordId={RecordId}", request.LaneNo, request.RecordId);
                    return BadRequest(CreateTrafficEventResponse(1, $"未匹配到车道：{request.LaneNo}"));
                }

                _trafficEventQueue.Enqueue(frm, plaza, lane, request);
                return Ok(CreateTrafficEventResponse(0, "推送成功"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "交通事件接收失败，LaneNo={LaneNo}, RecordId={RecordId}", request.LaneNo, request.RecordId);
                return BadRequest(CreateTrafficEventResponse(1, ex.Message));
            }
        }

        private bool TryResolveTrafficEventTarget(TrafficEventPushRequest request, out frmPlaza frm, out T_Plaza plaza, out T_Lane lane)
        {
            frm = null;
            plaza = null;
            lane = null;

            if (request == null || string.IsNullOrWhiteSpace(request.LaneNo) || option?.whoiam?.Plazas == null)
            {
                return false;
            }

            foreach (var currentPlaza in option.whoiam.Plazas)
            {
                var currentLane = currentPlaza?.Lanes?.FirstOrDefault(item => IsLaneMatch(item, request.LaneNo));
                if (currentLane == null)
                {
                    continue;
                }

                var currentForm = _cache.Get<frmPlaza>($"{nameof(frmPlaza)}_{currentPlaza.Id}");
                if (currentForm == null)
                {
                    continue;
                }

                frm = currentForm;
                plaza = currentPlaza;
                lane = currentLane;
                return true;
            }

            return false;
        }

        private static bool IsLaneMatch(T_Lane lane, string laneNo)
        {
            if (lane == null || string.IsNullOrWhiteSpace(laneNo))
            {
                return false;
            }

            return IsLaneTokenMatch(laneNo, lane.LaneNo)
                || IsLaneTokenMatch(laneNo, lane.LaneId);
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

        private static TrafficEventPushResponse CreateTrafficEventResponse(int code, string message)
        {
            return new TrafficEventPushResponse()
            {
                Code = code,
                Message = message,
                Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds()
            };
        }

    }
}

