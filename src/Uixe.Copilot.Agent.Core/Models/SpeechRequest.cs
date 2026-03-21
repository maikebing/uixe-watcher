namespace Uixe.Copilot.Agent.Core.Models;

public sealed record SpeechRequest(string Text, string? VoiceName = null, int Volume = 100, int Rate = 0);