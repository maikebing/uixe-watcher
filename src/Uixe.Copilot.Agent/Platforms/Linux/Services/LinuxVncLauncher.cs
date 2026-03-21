using Microsoft.Extensions.Options;
using Uixe.Copilot.Agent.Core.Abstractions;
using Uixe.Copilot.Agent.Core.Models;

namespace Uixe.Copilot.Agent.Platforms.Linux.Services;

public sealed class LinuxVncLauncher(IOptions<AgentOptions> options) : IVncLauncher
{
    public Task LaunchAsync(VncLaunchRequest request, CancellationToken cancellationToken = default)
    {
        var command = options.Value.LinuxVncCommand;
        var target = command.Equals("xdg-open", StringComparison.OrdinalIgnoreCase)
            ? request.ToUri()
            : request.Host;

        return LinuxCommandRunner.RunAsync(command, [target], cancellationToken);
    }
}