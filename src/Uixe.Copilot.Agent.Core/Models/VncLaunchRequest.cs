namespace Uixe.Copilot.Agent.Core.Models;

public sealed record VncLaunchRequest(string Host, int Port = 5900, string? Password = null, string? Title = null)
{
    public string ToUri() => $"vnc://{Host}:{Port}";
}