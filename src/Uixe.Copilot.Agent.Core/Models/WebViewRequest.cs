namespace Uixe.Copilot.Agent.Core.Models;

public sealed record WebViewRequest(string Url, string Title, int Width = 1280, int Height = 800, bool UseExternalBrowser = false);