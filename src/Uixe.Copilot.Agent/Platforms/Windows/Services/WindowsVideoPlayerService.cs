using Uixe.Copilot.Agent.Core.Abstractions;
using Uixe.Copilot.Agent.Core.Models;

namespace Uixe.Copilot.Agent.Platforms.Windows.Services;

public sealed class WindowsVideoPlayerService(IWindowsNativeHost nativeHost) : IVideoPlayerService
{
    public Task PlayAsync(VideoPlaybackRequest request, CancellationToken cancellationToken = default)
    {
        return nativeHost.LaunchShellAsync(request.Source, cancellationToken);
    }
}