using Uixe.Copilot.Agent.Core.Models;

namespace Uixe.Copilot.Agent.Core.Abstractions;

public interface ISpeechService
{
    Task SpeakAsync(SpeechRequest request, CancellationToken cancellationToken = default);
}