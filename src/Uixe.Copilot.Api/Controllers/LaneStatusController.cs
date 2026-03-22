using Microsoft.AspNetCore.Mvc;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Api.Controllers;

[ApiController]
[Route("api/lane-status")]
public sealed class LaneStatusController : ControllerBase
{
    private readonly ILaneStatusQueryService _laneStatusQueryService;

    public LaneStatusController(ILaneStatusQueryService laneStatusQueryService)
    {
        _laneStatusQueryService = laneStatusQueryService;
    }

    [HttpGet("plazas")]
    public async Task<ActionResult<IReadOnlyCollection<PlazaLaneSnapshotDto>>> GetPlazas(CancellationToken cancellationToken)
    {
        return Ok(await _laneStatusQueryService.GetPlazaLaneSnapshotsAsync(cancellationToken));
    }
}