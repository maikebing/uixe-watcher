namespace Uixe.Copilot.Contracts.Dtos;

public sealed class SystemSettingsDto
{
    public bool EnableVoiceBroadcast { get; set; }

    public bool EnableLocalNotification { get; set; }

    public bool EnableDarkTheme { get; set; }

    public bool EnableDesktopToast { get; set; }

    public bool EnableVncLaunch { get; set; }

    public bool EnableTrafficEventAudio { get; set; }

    public string PreferredVoiceName { get; set; } = string.Empty;

    public string PreferredTheme { get; set; } = "dark";

    public string TrafficEventStorageMode { get; set; } = "PostgreSQL";

    public string CurrentPhase { get; set; } = "Phase 2";

    public List<string> PhaseMilestones { get; set; } = new();
}