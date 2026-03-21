using Microsoft.Extensions.Options;
using Uixe.Copilot.Agent.Core.Abstractions;
using Uixe.Copilot.Agent.Core.Models;

namespace Uixe.Copilot.Agent.Platforms.Linux.Services;

public sealed class LinuxWebViewService(IOptions<AgentOptions> options) : IWebViewService
{
    public Task OpenAsync(WebViewRequest request, CancellationToken cancellationToken = default)
    {
        return LinuxCommandRunner.RunAsync(
            options.Value.LinuxBrowserCommand,
            [request.Url],
            cancellationToken);
    }
}