using System.Diagnostics;

namespace Uixe.Copilot.Agent.Platforms.Linux.Services;

internal static class LinuxCommandRunner
{
    public static Task RunAsync(string fileName, IEnumerable<string> arguments, CancellationToken cancellationToken = default)
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
}