using Uixe.Copilot.Agent.Core.Models;

namespace Uixe.Copilot.Agent.Core.Abstractions;

public interface IVideoPlayerService
{
    Task PlayAsync(VideoPlaybackRequest request, CancellationToken cancellationToken = default);
}