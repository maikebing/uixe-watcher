namespace Uixe.Copilot.Agent.Platforms.Windows.Services;

public interface IWindowsNativeHost
{
    Task LaunchProcessAsync(string fileName, IEnumerable<string> arguments, CancellationToken cancellationToken = default);

    Task LaunchShellAsync(string target, CancellationToken cancellationToken = default);
}