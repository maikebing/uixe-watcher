namespace Uixe.Copilot.Contracts.Dtos;

public sealed class TrafficEventOverviewDto
{
    public int OnlineStations { get; set; }

    public int TotalStations { get; set; }

    public int ActiveAlerts { get; set; }

    public int TodayEvents { get; set; }

    public int RealtimeMessages { get; set; }

    public List<int> Trend { get; set; } = new();

    public List<TrafficEventPlazaStatusDto> Plazas { get; set; } = new();

    public List<TrafficEventListItemDto> Events { get; set; } = new();
}
