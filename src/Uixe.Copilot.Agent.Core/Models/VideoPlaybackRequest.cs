namespace Uixe.Copilot.Agent.Core.Models;

public sealed record VideoPlaybackRequest(
    string Source,
    string Title,
    int Width = 1280,
    int Height = 720,
    bool AutoPlay = true,
    bool KeepWindowOpen = false);