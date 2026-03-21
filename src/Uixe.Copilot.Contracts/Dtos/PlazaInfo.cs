namespace Uixe.Copilot.Contracts.Dtos;

public sealed class PlazaInfo
{
    public string Id { get; set; } = string.Empty;

    public string StationName { get; set; } = string.Empty;

    public string? StationId { get; set; }

    public List<LaneInfo> Lanes { get; set; } = new();
}
