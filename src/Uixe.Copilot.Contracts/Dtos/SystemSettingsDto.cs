namespace Uixe.Copilot.Contracts.Dtos;

public sealed class SystemSettingsDto
{
    public bool EnableVoiceBroadcast { get; set; }

    public bool EnableLocalNotification { get; set; }

    public bool EnableDarkTheme { get; set; }

    public string CurrentPhase { get; set; } = "Phase 2";

    public List<string> PhaseMilestones { get; set; } = new();
}