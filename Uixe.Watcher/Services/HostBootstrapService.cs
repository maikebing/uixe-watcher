using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;
using Uixe.Watcher.Uitls;

namespace Uixe.Watcher.Services;

public sealed class HostBootstrapService : IHostBootstrapService
{
    public async Task<BossInfo?> ResolveBossAsync(string? laneBossServer, CancellationToken cancellationToken = default)
    {
        var result = await TollInfo.GuessMyInfo(laneBossServer ?? string.Empty);
        if (result?.code != 200 || result.data == null)
        {
            return null;
        }

        return result.data.ToBossInfo();
    }
}
