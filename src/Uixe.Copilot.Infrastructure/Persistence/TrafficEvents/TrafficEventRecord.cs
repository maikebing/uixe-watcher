namespace Uixe.Copilot.Infrastructure.Persistence.TrafficEvents;

public sealed class TrafficEventRecord
{
    public string Id { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string PlazaName { get; set; } = string.Empty;

    public string LaneNo { get; set; } = string.Empty;

    public string Level { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string? ImageUrl { get; set; }

    public string? VideoUrl { get; set; }

    public string ImageUrlsJson { get; set; } = "[]";

    public string VideoUrlsJson { get; set; } = "[]";

    public DateTime OccurredAt { get; set; }
}