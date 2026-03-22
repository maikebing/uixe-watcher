using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Contracts.Responses;

namespace Uixe.Copilot.Application.Abstractions;

public interface IAgentRegistryService
{
    Task<AgentRecordDto> RegisterAsync(AgentRegistrationRequestDto request, CancellationToken cancellationToken = default);

    Task<AgentRecordDto?> HeartbeatAsync(string agentId, AgentHeartbeatRequestDto request, CancellationToken cancellationToken = default);

    Task<ApiResult> UnregisterAsync(string agentId, CancellationToken cancellationToken = default);

    Task<AgentConfigDto> GetConfigAsync(string agentId, CancellationToken cancellationToken = default);

    Task<ApiResult> AckConfigAsync(string agentId, AgentConfigAckRequestDto request, CancellationToken cancellationToken = default);

    Task<ApiResult<AgentCommandRequestDto>> SubmitCommandAsync(AgentCommandRequestDto request, CancellationToken cancellationToken = default);

    Task<ApiResult<AgentCommandRequestDto>> GetCommandAsync(string commandId, CancellationToken cancellationToken = default);

    Task<ApiResult> AckCommandAsync(string commandId, AgentCommandAckRequestDto request, CancellationToken cancellationToken = default);
}