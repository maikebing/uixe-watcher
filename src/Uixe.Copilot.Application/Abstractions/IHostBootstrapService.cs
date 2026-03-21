using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Application.Abstractions;

public interface IHostBootstrapService
{
    Task<BossInfo?> ResolveBossAsync(string? laneBossServer, CancellationToken cancellationToken = default);
}
