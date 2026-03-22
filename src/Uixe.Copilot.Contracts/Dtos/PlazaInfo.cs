namespace Uixe.Copilot.Contracts.Dtos;

public sealed class PlazaInfo
{
    public string Id { get; set; } = string.Empty;

    public string? Ip { get; set; }

    public string StationName { get; set; } = string.Empty;

    public string? StationId { get; set; }

    public string? RoadId { get; set; }

    public string? RoadName { get; set; }

    public string? AgentIp { get; set; }

    public string? VncPassword { get; set; }

    public List<LaneInfo> Lanes { get; set; } = new();
}
