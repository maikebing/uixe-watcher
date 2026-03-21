using Microsoft.AspNetCore.Mvc;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Api.Controllers;

[ApiController]
[Route("api/traffic-events")]
public sealed class TrafficEventsController : ControllerBase
{
    private readonly ITrafficEventQueryService _queryService;

    public TrafficEventsController(ITrafficEventQueryService queryService)
    {
        _queryService = queryService;
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
}
