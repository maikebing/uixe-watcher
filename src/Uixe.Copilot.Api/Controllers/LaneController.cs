using Microsoft.AspNetCore.Mvc;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Contracts.Extensions;
using Uixe.Copilot.Contracts.Responses;

namespace Uixe.Copilot.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public sealed class LaneController : ControllerBase
{
    private readonly ILogger<LaneController> _logger;
    private readonly ILaneApplicationService _laneApplicationService;

    public LaneController(ILogger<LaneController> logger, ILaneApplicationService laneApplicationService)
    {
        _logger = logger;
        _laneApplicationService = laneApplicationService;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResult>> emrc_main_status(string plazaid, string laneno, LegacyLaneStatusRequestDto status, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _laneApplicationService.ShowLaneStatusAsync(plazaid, laneno, status.ToLaneStatusDto(), cancellationToken);
            return Ok(new ApiResult(ApiCode.OK, result.msg ?? "OK"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "处理车道状态失败，PlazaId={PlazaId}, LaneNo={LaneNo}", plazaid, laneno);
            return BadRequest(new ApiResult(ApiCode.BadRequest, ex.Message));
        }
    }

    [HttpPost]
    public async Task<ActionResult<ApiResult>> emrc_main_lost(string plazaid, string laneno, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _laneApplicationService.ShowLaneLostAsync(plazaid, laneno, cancellationToken);
            return Ok(new ApiResult(ApiCode.OK, result.msg ?? "OK"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "处理车道掉线失败，PlazaId={PlazaId}, LaneNo={LaneNo}", plazaid, laneno);
            return BadRequest(new ApiResult(ApiCode.BadRequest, ex.Message));
        }
    }

    [HttpPost]
    public async Task<ActionResult<ApiResult>> emrc_main_tco_weightAsync(string plazaid, TcoWeightMessageDto msgWeight, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _laneApplicationService.ShowWeightMessageAsync(plazaid, msgWeight, cancellationToken);
            return Ok(new ApiResult(ApiCode.OK, result.msg ?? "OK"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "处理称重 TCO 提示失败，PlazaId={PlazaId}", plazaid);
            return BadRequest(new ApiResult(ApiCode.BadRequest, ex.Message));
        }
    }

    [HttpPost]
    public async Task<ActionResult<ApiResult>> emrc_main_tco_confirmAsync(string plazaid, TcoConfirmRequestDto msg, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _laneApplicationService.ShowTcoConfirmAsync(plazaid, msg, cancellationToken);
            return Ok(new ApiResult(ApiCode.OK, result.msg ?? "OK"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "处理 TCO 确认失败，PlazaId={PlazaId}", plazaid);
            return BadRequest(new ApiResult(ApiCode.BadRequest, ex.Message));
        }
    }

    [HttpPost]
    public async Task<ActionResult<ApiResult>> emrc_main_message(string plazaid, LegacyLaneMessageRequestDto msg, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _laneApplicationService.ShowMessageAsync(plazaid, msg.ToLaneMessageDto(), cancellationToken);
            return Ok(new ApiResult(ApiCode.OK, result.msg ?? "OK"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "处理车道消息失败，PlazaId={PlazaId}", plazaid);
            return BadRequest(new ApiResult(ApiCode.BadRequest, ex.Message));
        }
    }

    [HttpPost]
    public async Task<ActionResult<ApiResult>> OverloadAlarm(string plazaid, LegacyOverloadWarningRequestDto warn, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _laneApplicationService.ShowOverloadAlarmAsync(plazaid, warn.ToOverloadWarningDto(), true, cancellationToken);
            return Ok(new ApiResult(ApiCode.OK, result.msg ?? "OK"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "处理超限告警失败，PlazaId={PlazaId}", plazaid);
            return BadRequest(new ApiResult(ApiCode.BadRequest, ex.Message));
        }
    }

    [HttpPost]
    public async Task<ActionResult<ApiResult>> Lanespecial(string plazaid, LegacyLaneSpecialRequestDto msg, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _laneApplicationService.ShowLaneSpecialAsync(plazaid, msg.ToLaneSpecialDto(), cancellationToken);
            return Ok(new ApiResult(ApiCode.OK, result.msg ?? "OK"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "处理车道特情失败，PlazaId={PlazaId}", plazaid);
            return BadRequest(new ApiResult(ApiCode.BadRequest, ex.Message));
        }
    }

    [HttpPost]
    public async Task<ActionResult<ApiResult>> Bulktrans(string plazaid, BulkTransportDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _laneApplicationService.ShowBulkTransAsync(plazaid, dto, cancellationToken);
            return Ok(new ApiResult(ApiCode.OK, result.msg ?? "OK"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "处理大件运输失败，PlazaId={PlazaId}", plazaid);
            return BadRequest(new ApiResult(ApiCode.BadRequest, ex.Message));
        }
    }

    [HttpPost]
    public async Task<ActionResult<ApiResult>> bill_info(string plazaid, BillInfoRequestDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _laneApplicationService.ShowBillInfoAsync(plazaid, dto, cancellationToken);
            return Ok(new ApiResult(ApiCode.OK, result.msg ?? "OK"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "处理发票信息失败，PlazaId={PlazaId}", plazaid);
            return BadRequest(new ApiResult(ApiCode.BadRequest, ex.Message));
        }
    }

    [HttpPost]
    public async Task<ActionResult<ApiResult>> ConfirmEnInfo(string plazaId, ConfirmEnInfoDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _laneApplicationService.ShowConfirmEnInfoAsync(plazaId, dto, cancellationToken);
            return Ok(new ApiResult(ApiCode.OK, result.msg ?? "OK"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "处理入口确认失败，PlazaId={PlazaId}", plazaId);
            return BadRequest(new ApiResult(ApiCode.BadRequest, ex.Message));
        }
    }

    [HttpPost]
    public async Task<ActionResult<LegacyTrafficEventPushResponseDto>> TrafficEvent([FromBody] TrafficEventPushRequestDto request, CancellationToken cancellationToken)
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
            var response = await _laneApplicationService.EnqueueTrafficEventAsync(request, cancellationToken);

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

    private static LegacyTrafficEventPushResponseDto CreateTrafficEventResponse(int code, string message)
    {
        return new LegacyTrafficEventPushResponseDto
        {
            Code = code,
            Message = message,
            Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds()
        };
    }
}