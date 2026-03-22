using Uixe.Copilot.Agent.Core.Abstractions;
using Uixe.Copilot.Agent.Core.Models;

namespace Uixe.Copilot.Agent.Platforms.Windows.Services;

public sealed class WindowsSpeechService(IWindowsNativeHost nativeHost) : ISpeechService
{
    public Task SpeakAsync(SpeechRequest request, CancellationToken cancellationToken = default)
    {
        var script = BuildSpeechScript(request);

        return nativeHost.LaunchProcessAsync("powershell", ["-NoProfile", "-Command", script], cancellationToken);
    }

    private static string BuildSpeechScript(SpeechRequest request)
    {
        var scriptSegments = new List<string>
        {
            "Add-Type -AssemblyName System.Speech",
            "$synth = New-Object System.Speech.Synthesis.SpeechSynthesizer",
            $"$synth.Volume = {Math.Clamp(request.Volume, 0, 100)}",
            $"$synth.Rate = {Math.Clamp(request.Rate, -10, 10)}"
        };

        if (!string.IsNullOrWhiteSpace(request.VoiceName))
        {
            scriptSegments.Add($"$synth.SelectVoice('{EscapePowerShellString(request.VoiceName)}')");
        }

        scriptSegments.Add($"$synth.Speak('{EscapePowerShellString(request.Text)}')");
        return string.Join("; ", scriptSegments);
    }

    private static string EscapePowerShellString(string value)
    {
        return value.Replace("'", "''", StringComparison.Ordinal);
    }
}
