using Uixe.Copilot.Agent.Core.Abstractions;
using Uixe.Copilot.Agent.Core.Models;

namespace Uixe.Copilot.Agent.Platforms.Windows.Services;

public sealed class WindowsWebViewService(IWindowsNativeHost nativeHost) : IWebViewService
{
    public Task OpenAsync(WebViewRequest request, CancellationToken cancellationToken = default)
    {
        return nativeHost.LaunchShellAsync(request.Url, cancellationToken);
    }
}