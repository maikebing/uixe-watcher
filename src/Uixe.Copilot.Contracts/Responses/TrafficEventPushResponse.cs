using System.Text.Json.Serialization;

namespace Uixe.Copilot.Contracts.Responses;

public sealed class TrafficEventPushResponse
{
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("timestamp")]
    public long Timestamp { get; set; }
}
