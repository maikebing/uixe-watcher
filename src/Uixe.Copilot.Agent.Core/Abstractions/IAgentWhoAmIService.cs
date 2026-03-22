using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Agent.Core.Abstractions;

public interface IAgentWhoAmIService
{
    Task<BossInfo?> ResolveAndCacheAsync(CancellationToken cancellationToken = default);

    BossInfo? GetCachedBoss();
}
