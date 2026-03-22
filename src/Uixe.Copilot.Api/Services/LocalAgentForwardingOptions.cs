namespace Uixe.Copilot.Api.Services;

public sealed class LocalAgentForwardingOptions
{
    public const string SectionName = "LocalAgentForwarding";

    public string BaseUrl { get; set; } = "http://127.0.0.1:17173";
}