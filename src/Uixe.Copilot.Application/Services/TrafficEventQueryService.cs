using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Application.Services;

public sealed class TrafficEventQueryService : ITrafficEventQueryService
{
    private readonly ITrafficEventRepository _trafficEventRepository;

    public TrafficEventQueryService(ITrafficEventRepository trafficEventRepository)
    {
        _trafficEventRepository = trafficEventRepository;
    }

    public Task<TrafficEventOverviewDto> GetOverviewAsync(CancellationToken cancellationToken = default)
    {
        return BuildOverviewAsync(cancellationToken);
    }

    public async Task<TrafficEventListItemDto?> GetByIdAsync(string eventId, CancellationToken cancellationToken = default)
    {
        return await _trafficEventRepository.GetByIdAsync(eventId, cancellationToken);
    }

    private async Task<TrafficEventOverviewDto> BuildOverviewAsync(CancellationToken cancellationToken)
    {
        var events = (await _trafficEventRepository.GetRecentEventsAsync(cancellationToken)).ToList();
        var activeAlerts = events.Count(x => string.Equals(x.Status, "待处理", StringComparison.OrdinalIgnoreCase) || string.Equals(x.Status, "处理中", StringComparison.OrdinalIgnoreCase));

        return new TrafficEventOverviewDto
        {
            OnlineStations = 24,
            TotalStations = 28,
            ActiveAlerts = activeAlerts,
            TodayEvents = events.Count,
            RealtimeMessages = events.Count,
            Trend = BuildTrend(events.Count),
            Plazas = new List<TrafficEventPlazaStatusDto>
            {
                new() { Id = "6500256", Name = "城北收费站", Status = "online", LanesOnline = 8, LanesTotal = 10, Alerts = 0 },
                new() { Id = "6500257", Name = "高新收费站", Status = "online", LanesOnline = 6, LanesTotal = 8, Alerts = 0 },
                new() { Id = "6500258", Name = "机场收费站", Status = "online", LanesOnline = 12, LanesTotal = 12, Alerts = 0 }
            },
            Events = events
        };
    }

    private static List<int> BuildTrend(int total)
    {
        return new List<int>
        {
            Math.Max(0, total - 6),
            Math.Max(0, total - 5),
            Math.Max(0, total - 4),
            Math.Max(0, total - 3),
            Math.Max(0, total - 2),
            Math.Max(0, total - 1),
            total
        };
    }
}
