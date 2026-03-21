using System.Speech.Synthesis;
using Uixe.Copilot.Agent.Core.Abstractions;
using Uixe.Copilot.Agent.Core.Models;

namespace Uixe.Copilot.Agent.Platforms.Windows.Services;

public sealed class WindowsSpeechService : ISpeechService
{
    public Task SpeakAsync(SpeechRequest request, CancellationToken cancellationToken = default)
    {
        using var synth = new SpeechSynthesizer();
        synth.Volume = Math.Clamp(request.Volume, 0, 100);
        synth.Rate = Math.Clamp(request.Rate, -10, 10);

        if (!string.IsNullOrWhiteSpace(request.VoiceName))
        {
            synth.SelectVoice(request.VoiceName);
        }

        synth.Speak(request.Text);
        return Task.CompletedTask;
    }
}