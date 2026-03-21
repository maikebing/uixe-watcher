using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;
using Uixe.Watcher.TCO;
using Uixe.Watcher.Services;
namespace Uixe.Watcher.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public partial class LaneController : ControllerBase
    {

        private readonly ILogger<LaneController> _logger;
        private readonly ILaneApplicationService _laneApplicationService;

        public LaneController(ILogger<LaneController> logger, ILaneApplicationService laneApplicationService)
        {
            _logger = logger;
            _laneApplicationService = laneApplicationService;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult>> emrc_main_status(string plazaid, string laneno, LaneStatus status)
        {
            try
            {
                var result = await _laneApplicationService.ShowLaneStatusAsync(plazaid, laneno, status);
                return Ok(new ApiResult(ApiCode.OK, result.msg ?? "OK"));
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
                var result = await _laneApplicationService.ShowWeightMessageAsync(plazaid, msgWeight);
                return Ok(new ApiResult(ApiCode.OK, result.msg ?? "OK"));
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
                var result = await _laneApplicationService.ShowTcoConfirmAsync(plazaid, msg);
                return Ok(new ApiResult(ApiCode.OK, result.msg ?? "OK"));
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

                var result = await _laneApplicationService.ShowMessageAsync(plazaid, msg);
                return Ok(new ApiResult(ApiCode.OK, result.msg ?? "OK"));
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
                var result = await _laneApplicationService.ShowOverloadAlarmAsync(plazaid, warn.Title, warn.Context, true);
                return Ok(new ApiResult(ApiCode.OK, result.msg ?? "OK"));
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
                var result = await _laneApplicationService.ShowLaneSpecialAsync(plazaid, msg);
                return Ok(new ApiResult(ApiCode.OK, result.msg ?? "OK"));
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
                                var result = await _laneApplicationService.ShowBulkTransAsync(plazaid, dto);
                                return Ok(new ApiResult(ApiCode.OK, result.msg ?? "OK"));

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
                var result = await _laneApplicationService.ShowBillInfoAsync(plazaid, dto);
                return Ok(new ApiResult(ApiCode.OK, result.msg ?? "OK"));

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
                var result = await _laneApplicationService.ShowConfirmEnInfoAsync(plazaId, dto);
                return Ok(new ApiResult(ApiCode.OK, result.msg ?? "OK"));

            }
            catch (Exception)
            {
                return BadRequest(new ApiResult(ApiCode.BadRequst, "OK"));
            }
        }

        [HttpPost]
        public async Task<ActionResult<TrafficEventPushResponse>> TrafficEvent([FromBody] TrafficEventPushRequest request)
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
                var response = await _laneApplicationService.EnqueueTrafficEventAsync(new TrafficEventPushRequestDto
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
                });

                return response.Code == 0
                    ? Ok(CreateTrafficEventResponse(response.Code, response.Message))
                    : BadRequest(CreateTrafficEventResponse(response.Code, response.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "交通事件接收失败，LaneNo={LaneNo}, RecordId={RecordId}", request.LaneNo, request.RecordId);
                return BadRequest(CreateTrafficEventResponse(1, ex.Message));
            }
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

