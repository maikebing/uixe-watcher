namespace Uixe.Copilot.Agent.Core.Models;

public sealed record AgentCommand(
    bool KeepRunning,
    LocalNotificationRequest? Notification,
    SpeechRequest? Speech,
    VncLaunchRequest? Vnc,
    WebViewRequest? WebView)
{
    public bool HasOperations => Notification is not null || Speech is not null || Vnc is not null || WebView is not null;
}