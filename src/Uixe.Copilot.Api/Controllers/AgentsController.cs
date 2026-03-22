using Microsoft.AspNetCore.Mvc;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Contracts.Responses;

namespace Uixe.Copilot.Api.Controllers;

[ApiController]
[Route("api/agents")]
public sealed class AgentsController : ControllerBase
{
    private readonly IAgentRegistryService _agentRegistryService;

    public AgentsController(IAgentRegistryService agentRegistryService)
    {
        _agentRegistryService = agentRegistryService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AgentRecordDto>> Register([FromBody] AgentRegistrationRequestDto request, CancellationToken cancellationToken)
    {
        return Ok(await _agentRegistryService.RegisterAsync(request, cancellationToken));
    }

    [HttpPut("{agentId}/heartbeat")]
    public async Task<ActionResult<AgentRecordDto>> Heartbeat(string agentId, [FromBody] AgentHeartbeatRequestDto request, CancellationToken cancellationToken)
    {
        var result = await _agentRegistryService.HeartbeatAsync(agentId, request, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("{agentId}")]
    public async Task<ActionResult<ApiResult>> Unregister(string agentId, CancellationToken cancellationToken)
    {
        var result = await _agentRegistryService.UnregisterAsync(agentId, cancellationToken);
        return result.code == (int)ApiCode.NotFound ? NotFound(result) : Ok(result);
    }

    [HttpGet("{agentId}/config")]
    public async Task<ActionResult<AgentConfigDto>> GetConfig(string agentId, CancellationToken cancellationToken)
    {
        return Ok(await _agentRegistryService.GetConfigAsync(agentId, cancellationToken));
    }

    [HttpPut("{agentId}/config-ack")]
    public async Task<ActionResult<ApiResult>> AckConfig(string agentId, [FromBody] AgentConfigAckRequestDto request, CancellationToken cancellationToken)
    {
        return Ok(await _agentRegistryService.AckConfigAsync(agentId, request, cancellationToken));
    }

    [HttpPost("commands")]
    public async Task<ActionResult<ApiResult<AgentCommandRequestDto>>> SubmitCommand([FromBody] AgentCommandRequestDto request, CancellationToken cancellationToken)
    {
        return Ok(await _agentRegistryService.SubmitCommandAsync(request, cancellationToken));
    }

    [HttpGet("commands/{commandId}")]
    public async Task<ActionResult<ApiResult<AgentCommandRequestDto>>> GetCommand(string commandId, CancellationToken cancellationToken)
    {
        var result = await _agentRegistryService.GetCommandAsync(commandId, cancellationToken);
        return result.code == (int)ApiCode.NotFound ? NotFound(result) : Ok(result);
    }

    [HttpPost("commands/{commandId}/ack")]
    public async Task<ActionResult<ApiResult>> AckCommand(string commandId, [FromBody] AgentCommandAckRequestDto request, CancellationToken cancellationToken)
    {
        var result = await _agentRegistryService.AckCommandAsync(commandId, request, cancellationToken);
        return result.code == (int)ApiCode.NotFound ? NotFound(result) : Ok(result);
    }
}