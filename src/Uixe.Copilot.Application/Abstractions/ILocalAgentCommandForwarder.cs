using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Contracts.Responses;

namespace Uixe.Copilot.Application.Abstractions;

public interface ILocalAgentCommandForwarder
{
    Task<ApiResult<AgentCommandAckRequestDto>> ForwardAsync(AgentCommandRequestDto request, CancellationToken cancellationToken = default);
}