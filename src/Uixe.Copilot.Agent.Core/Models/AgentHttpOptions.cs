namespace Uixe.Copilot.Agent.Core.Models;

public sealed class AgentHttpOptions
{
    public const string SectionName = "AgentHttp";

    public string ListenUrl { get; set; } = "http://127.0.0.1:17173/";
}