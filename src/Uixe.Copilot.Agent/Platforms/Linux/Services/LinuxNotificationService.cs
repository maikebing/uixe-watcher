using Microsoft.Extensions.Options;
using Uixe.Copilot.Agent.Core.Abstractions;
using Uixe.Copilot.Agent.Core.Models;

namespace Uixe.Copilot.Agent.Platforms.Linux.Services;

public sealed class LinuxNotificationService(IOptions<AgentOptions> options) : ILocalNotificationService
{
    public Task ShowAsync(LocalNotificationRequest request, CancellationToken cancellationToken = default)
    {
        return LinuxCommandRunner.RunAsync(
            options.Value.LinuxNotificationCommand,
            [request.Title, request.Message],
            cancellationToken);
    }
}