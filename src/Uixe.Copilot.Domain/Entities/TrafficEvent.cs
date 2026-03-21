namespace Uixe.Copilot.Domain.Entities;

public sealed class TrafficEvent
{
    public string Id { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string PlazaName { get; set; } = string.Empty;

    public string LaneNo { get; set; } = string.Empty;

    public string Level { get; set; } = "medium";

    public string Status { get; set; } = "´ý´¦Àí";

    public string? ImageUrl { get; set; }

    public string? VideoUrl { get; set; }

    public List<string> ImageUrls { get; set; } = new();

    public List<string> VideoUrls { get; set; } = new();

    public DateTime OccurredAt { get; set; }
}