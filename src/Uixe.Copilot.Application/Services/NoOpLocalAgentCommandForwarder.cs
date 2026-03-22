using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Contracts.Responses;

namespace Uixe.Copilot.Application.Services;

public sealed class NoOpLocalAgentCommandForwarder : ILocalAgentCommandForwarder
{
    public Task<ApiResult<AgentCommandAckRequestDto>> ForwardAsync(AgentCommandRequestDto request, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new ApiResult<AgentCommandAckRequestDto>
        {
            code = (int)ApiCode.OK,
            msg = "local agent forwarder not enabled, command stored only",
            data = new AgentCommandAckRequestDto
            {
                CommandId = request.CommandId,
                AgentId = request.TargetAgentId ?? "local-agent",
                Status = "accepted",
                Message = "command accepted but not forwarded"
            }
        });
    }
}