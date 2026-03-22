using Microsoft.Extensions.Options;
using Uixe.Copilot.Agent.Core.Abstractions;
using Uixe.Copilot.Agent.Core.Models;

namespace Uixe.Copilot.Agent.Platforms.Linux.Services;

public sealed class LinuxVideoPlayerService(IOptions<AgentOptions> options) : IVideoPlayerService
{
    public Task PlayAsync(VideoPlaybackRequest request, CancellationToken cancellationToken = default)
    {
        return LinuxCommandRunner.RunAsync(
            options.Value.LinuxVideoPlayerCommand,
            [request.Source],
            cancellationToken);
    }
}