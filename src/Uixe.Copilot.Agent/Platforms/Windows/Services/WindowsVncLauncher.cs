using System.Diagnostics;
using Uixe.Copilot.Agent.Core.Abstractions;
using Uixe.Copilot.Agent.Core.Models;

namespace Uixe.Copilot.Agent.Platforms.Windows.Services;

public sealed class WindowsVncLauncher(IWindowsNativeHost nativeHost) : IVncLauncher
{
    public Task LaunchAsync(VncLaunchRequest request, CancellationToken cancellationToken = default)
    {
        return nativeHost.LaunchShellAsync(request.ToUri(), cancellationToken);
    }
}