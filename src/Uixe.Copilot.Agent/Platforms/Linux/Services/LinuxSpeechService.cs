using Microsoft.Extensions.Options;
using Uixe.Copilot.Agent.Core.Abstractions;
using Uixe.Copilot.Agent.Core.Models;

namespace Uixe.Copilot.Agent.Platforms.Linux.Services;

public sealed class LinuxSpeechService(IOptions<AgentOptions> options) : ISpeechService
{
    public Task SpeakAsync(SpeechRequest request, CancellationToken cancellationToken = default)
    {
        return LinuxCommandRunner.RunAsync(
            options.Value.LinuxSpeechCommand,
            [request.Text],
            cancellationToken);
    }
}