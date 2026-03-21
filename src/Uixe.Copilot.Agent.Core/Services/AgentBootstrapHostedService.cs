using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Uixe.Copilot.Agent.Core.Abstractions;
using Uixe.Copilot.Agent.Core.Models;

namespace Uixe.Copilot.Agent.Core.Services;

public sealed class AgentBootstrapHostedService(IAgentTrayService trayService, IOptions<AgentOptions> options) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        return trayService.InitializeAsync(new TrayIconOptions(options.Value.TrayTooltip), cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}