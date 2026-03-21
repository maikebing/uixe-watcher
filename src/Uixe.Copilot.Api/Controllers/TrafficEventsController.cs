using Microsoft.AspNetCore.Mvc;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Api.Controllers;

[ApiController]
[Route("api/traffic-events")]
public sealed class TrafficEventsController : ControllerBase
{
    private readonly ITrafficEventQueryService _queryService;
    private readonly ITrafficEventApplicationService _applicationService;

    public TrafficEventsController(ITrafficEventQueryService queryService, ITrafficEventApplicationService applicationService)
    {
        _queryService = queryService;
        _applicationService = applicationService;
    }

    [HttpGet("overview")]
    public async Task<ActionResult<TrafficEventOverviewDto>> GetOverview(CancellationToken cancellationToken)
    {
        return Ok(await _queryService.GetOverviewAsync(cancellationToken));
    }

    [HttpGet("{eventId}")]
    public async Task<ActionResult<TrafficEventListItemDto>> GetById(string eventId, CancellationToken cancellationToken)
    {
        var eventItem = await _queryService.GetByIdAsync(eventId, cancellationToken);
        return eventItem is null ? NotFound() : Ok(eventItem);
    }

    [HttpPost]
    public async Task<ActionResult<TrafficEventPushResponse>> Submit([FromBody] TrafficEventPushRequestDto request, CancellationToken cancellationToken)
    {
        var response = await _applicationService.SubmitAsync(request, cancellationToken);
        return response.Code == 0 ? Ok(response) : BadRequest(response);
    }
}
