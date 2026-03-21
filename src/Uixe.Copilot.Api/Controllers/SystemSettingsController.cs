using Microsoft.AspNetCore.Mvc;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Api.Controllers;

[ApiController]
[Route("api/system-settings")]
public sealed class SystemSettingsController : ControllerBase
{
    private readonly ISystemSettingsService _systemSettingsService;

    public SystemSettingsController(ISystemSettingsService systemSettingsService)
    {
        _systemSettingsService = systemSettingsService;
    }

    [HttpGet]
    public async Task<ActionResult<SystemSettingsDto>> Get(CancellationToken cancellationToken)
    {
        return Ok(await _systemSettingsService.GetAsync(cancellationToken));
    }

    [HttpPut]
    public async Task<ActionResult<SystemSettingsDto>> Save([FromBody] SystemSettingsDto settings, CancellationToken cancellationToken)
    {
        return Ok(await _systemSettingsService.SaveAsync(settings, cancellationToken));
    }
}