using Uixe.Copilot.Agent.Core.Models;

namespace Uixe.Copilot.Agent.Core.Abstractions;

public interface IAgentInstructionDispatcher
{
    Task<AgentInstructionResponse> DispatchAsync(AgentInstructionRequest request, CancellationToken cancellationToken = default);
}