namespace Uixe.Copilot.Agent.Core.Models;

public sealed class AgentOptions
{
    public const string SectionName = "Agent";

    public string ApplicationName { get; set; } = "Uixe.Copilot.Agent";

    public string TrayTooltip { get; set; } = "Uixe.Copilot.Agent";

    public string WebDashboardUrl { get; set; } = "http://127.0.0.1:9999";

    public string LaneBossServer { get; set; } = "http://127.0.0.1/";

    public string LinuxTrayCommand { get; set; } = "zenity";

    public string LinuxSpeechCommand { get; set; } = "spd-say";

    public string LinuxNotificationCommand { get; set; } = "notify-send";

    public string LinuxBrowserCommand { get; set; } = "xdg-open";

    public string LinuxVncCommand { get; set; } = "xdg-open";

    public string LinuxVideoPlayerCommand { get; set; } = "xdg-open";

    public bool ForceLocalhostInDebugBuild { get; set; } = true;
}
