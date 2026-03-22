namespace Uixe.Copilot.Agent.Core.Models;

public sealed class AgentInstructionRequest
{
    public string CommandType { get; set; } = string.Empty;

    public string? Title { get; set; }

    public string? Message { get; set; }

    public string? Text { get; set; }

    public string? VoiceName { get; set; }

    public int? Volume { get; set; }

    public int? Rate { get; set; }

    public bool PlaySpeech { get; set; }

    public string? Host { get; set; }

    public int? Port { get; set; }

    public string? Password { get; set; }

    public string? VncTitle { get; set; }

    public string? Url { get; set; }

    public string? WebTitle { get; set; }

    public string? VideoSource { get; set; }

    public string? VideoTitle { get; set; }

    public int? Width { get; set; }

    public int? Height { get; set; }

    public bool AutoPlay { get; set; } = true;

    public bool KeepWindowOpen { get; set; }

    public bool KeepRunning { get; set; } = true;
}