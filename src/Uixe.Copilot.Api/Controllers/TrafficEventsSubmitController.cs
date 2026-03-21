using Microsoft.AspNetCore.Mvc;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Contracts.Responses;

namespace Uixe.Copilot.Api.Controllers;

[ApiController]
[Route("api/traffic-events")]
public sealed partial class TrafficEventsController : ControllerBase
{
    private readonly ITrafficEventApplicationService _applicationService;

    [HttpPost]
    public async Task<ActionResult<TrafficEventPushResponse>> Submit([FromBody] TrafficEventPushRequestDto request, CancellationToken cancellationToken)
    {
        var response = await _applicationService.SubmitAsync(request, cancellationToken);
        return response.Code == 0 ? Ok(response) : BadRequest(response);
    }
}
