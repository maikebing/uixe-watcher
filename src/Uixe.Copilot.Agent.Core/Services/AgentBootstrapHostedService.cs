using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Uixe.Copilot.Agent.Core.Abstractions;
using Uixe.Copilot.Agent.Core.Models;

namespace Uixe.Copilot.Agent.Core.Services;

public sealed class AgentBootstrapHostedService(
    IAgentTrayService trayService,
    IAgentWhoAmIService whoAmIService,
    IOptions<AgentOptions> options,
    ILogger<AgentBootstrapHostedService> logger) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await trayService.InitializeAsync(new TrayIconOptions(options.Value.TrayTooltip), cancellationToken);

        var boss = await whoAmIService.ResolveAndCacheAsync(cancellationToken);
        if (boss is null)
        {
            logger.LogWarning("Agent startup could not resolve local station identity from {LaneBossServer}.", options.Value.LaneBossServer);
            return;
        }

        var plaza = boss.Plazas.FirstOrDefault();
        logger.LogInformation(
            "Agent startup resolved station identity. Boss={BossName}, Plaza={PlazaName}, StationId={StationId}, AgentIp={AgentIp}.",
            boss.Name,
            plaza?.StationName,
            plaza?.StationId,
            boss.AgentIp);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
