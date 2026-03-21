using System.Text.Json.Serialization;

namespace Uixe.Copilot.Contracts.Dtos;

public sealed class TrafficEventPushRequestDto
{
    [JsonPropertyName("recordId")]
    public string? RecordId { get; set; }

    [JsonPropertyName("eventType")]
    public string? EventType { get; set; }

    [JsonPropertyName("LaneNo")]
    public string? LaneNo { get; set; }

    [JsonPropertyName("capTime")]
    public DateTime? CapTime { get; set; }

    [JsonPropertyName("startTime")]
    public DateTime? StartTime { get; set; }

    [JsonPropertyName("period")]
    public int? Period { get; set; }

    [JsonPropertyName("periodByMili")]
    public int? PeriodByMili { get; set; }

    [JsonPropertyName("maxQueueLen")]
    public float? MaxQueueLen { get; set; }

    [JsonPropertyName("imageList")]
    public string? ImageList { get; set; }

    [JsonPropertyName("videoList")]
    public string? VideoList { get; set; }
}
