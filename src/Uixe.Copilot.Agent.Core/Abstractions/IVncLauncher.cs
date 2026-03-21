using Uixe.Copilot.Agent.Core.Models;

namespace Uixe.Copilot.Agent.Core.Abstractions;

public interface IVncLauncher
{
    Task LaunchAsync(VncLaunchRequest request, CancellationToken cancellationToken = default);
}