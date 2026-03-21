using Uixe.Copilot.Agent.Core.Models;

namespace Uixe.Copilot.Agent.Core.Abstractions;

public interface IWebViewService
{
    Task OpenAsync(WebViewRequest request, CancellationToken cancellationToken = default);
}