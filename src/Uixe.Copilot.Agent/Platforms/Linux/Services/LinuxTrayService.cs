using Microsoft.Extensions.Options;
using Uixe.Copilot.Agent.Core.Abstractions;
using Uixe.Copilot.Agent.Core.Models;

namespace Uixe.Copilot.Agent.Platforms.Linux.Services;

public sealed class LinuxTrayService(IOptions<AgentOptions> options) : IAgentTrayService
{
    private Task? _trayTask;

    public Task InitializeAsync(TrayIconOptions trayOptions, CancellationToken cancellationToken = default)
    {
        _trayTask ??= LinuxCommandRunner.RunAsync(
            options.Value.LinuxTrayCommand,
            ["--notification", "--text", trayOptions.Tooltip],
            cancellationToken);

        return Task.CompletedTask;
    }

    public ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }
}