using Uixe.Copilot.Agent.Core.Abstractions;
using Uixe.Copilot.Agent.Core.Models;

namespace Uixe.Copilot.Agent.Platforms.Windows.Services;

public sealed class WindowsTrayService(IWindowsNativeHost nativeHost) : IAgentTrayService
{
    public Task InitializeAsync(TrayIconOptions options, CancellationToken cancellationToken = default)
    {
        return nativeHost.LaunchProcessAsync("powershell", ["-NoProfile", "-WindowStyle", "Hidden", "-Command", "$Host.UI.RawUI.WindowTitle='Uixe.Copilot.Agent Tray'"] , cancellationToken);
    }

    public ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }
}