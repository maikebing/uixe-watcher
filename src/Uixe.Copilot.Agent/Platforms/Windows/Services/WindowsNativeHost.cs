using System.Diagnostics;

namespace Uixe.Copilot.Agent.Platforms.Windows.Services;

public sealed class WindowsNativeHost : IWindowsNativeHost
{
    public Task LaunchProcessAsync(string fileName, IEnumerable<string> arguments, CancellationToken cancellationToken = default)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = fileName,
            UseShellExecute = false,
            RedirectStandardError = false,
            RedirectStandardOutput = false
        };

        foreach (var argument in arguments)
        {
            startInfo.ArgumentList.Add(argument);
        }

        using var process = Process.Start(startInfo) ?? throw new InvalidOperationException($"Unable to start process '{fileName}'.");
        return process.WaitForExitAsync(cancellationToken);
    }

    public Task LaunchShellAsync(string target, CancellationToken cancellationToken = default)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = target,
            UseShellExecute = true
        });

        return Task.CompletedTask;
    }
}