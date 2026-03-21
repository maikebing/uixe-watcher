using Uixe.Copilot.Agent.Core.Models;

namespace Uixe.Copilot.Agent.Core.Abstractions;

public interface IAgentTrayService : IAsyncDisposable
{
    Task InitializeAsync(TrayIconOptions options, CancellationToken cancellationToken = default);
}