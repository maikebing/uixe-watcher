namespace Uixe.Copilot.Contracts.Dtos;

public sealed class TrafficEventListItemDto
{
    public string Id { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string PlazaName { get; set; } = string.Empty;

    public string LaneNo { get; set; } = string.Empty;

    public string Level { get; set; } = "medium";

    public string Time { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string? ImageUrl { get; set; }

    public string? VideoUrl { get; set; }
}
