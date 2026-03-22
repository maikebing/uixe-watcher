using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Application.Services;

public sealed class TrafficEventQueryService : ITrafficEventQueryService
{
    private readonly IPlazaContextService _plazaContextService;
    private readonly ILaneStatusSnapshotStore _laneStatusSnapshotStore;
    private readonly ITrafficEventRepository _trafficEventRepository;

    public TrafficEventQueryService(
        ITrafficEventRepository trafficEventRepository,
        IPlazaContextService plazaContextService,
        ILaneStatusSnapshotStore laneStatusSnapshotStore)
    {
        _trafficEventRepository = trafficEventRepository;
        _plazaContextService = plazaContextService;
        _laneStatusSnapshotStore = laneStatusSnapshotStore;
    }

    public Task<TrafficEventOverviewDto> GetOverviewAsync(CancellationToken cancellationToken = default)
    {
        return BuildOverviewAsync(cancellationToken);
    }

    public async Task<TrafficEventListItemDto?> GetByIdAsync(string eventId, CancellationToken cancellationToken = default)
    {
        return await _trafficEventRepository.GetByIdAsync(eventId, cancellationToken);
    }

    public async Task<TrafficEventHistoryResponseDto> GetHistoryAsync(TrafficEventHistoryQueryDto query, CancellationToken cancellationToken = default)
    {
        var items = (await _trafficEventRepository.QueryAsync(query, cancellationToken)).ToList();
        var total = await _trafficEventRepository.CountAsync(query, cancellationToken);
        return new TrafficEventHistoryResponseDto
        {
            Total = total,
            PageNo = query.PageNo,
            PageSize = query.PageSize,
            Items = items
        };
    }

    private async Task<TrafficEventOverviewDto> BuildOverviewAsync(CancellationToken cancellationToken)
    {
        var events = (await _trafficEventRepository.GetRecentEventsAsync(cancellationToken)).ToList();
        var activeAlerts = events.Count(x => string.Equals(x.Status, "待处理", StringComparison.OrdinalIgnoreCase) || string.Equals(x.Status, "处理中", StringComparison.OrdinalIgnoreCase));
        var plazas = _plazaContextService.GetPlazas();
        var snapshots = _laneStatusSnapshotStore.GetAll().ToDictionary(item => item.PlazaId, StringComparer.OrdinalIgnoreCase);

        var plazaItems = plazas.Select(plaza =>
        {
            var plazaId = plaza.Id ?? string.Empty;
            if (snapshots.TryGetValue(plazaId, out var snapshot))
            {
                return new TrafficEventPlazaStatusDto
                {
                    Id = plazaId,
                    Name = plaza.StationName ?? plazaId,
                    Status = snapshot.Alerts > 0 ? "warning" : snapshot.LanesOnline > 0 ? "online" : "offline",
                    LanesOnline = snapshot.LanesOnline,
                    LanesTotal = Math.Max(snapshot.LanesTotal, plaza.Lanes.Count),
                    Alerts = snapshot.Alerts
                };
            }

            return new TrafficEventPlazaStatusDto
            {
                Id = plazaId,
                Name = plaza.StationName ?? plazaId,
                Status = "offline",
                LanesOnline = 0,
                LanesTotal = plaza.Lanes.Count,
                Alerts = 0
            };
        }).ToList();

        var totalStations = plazaItems.Count;
        var onlineStations = plazaItems.Count(item => !string.Equals(item.Status, "offline", StringComparison.OrdinalIgnoreCase));

        return new TrafficEventOverviewDto
        {
            OnlineStations = onlineStations,
            TotalStations = totalStations,
            ActiveAlerts = activeAlerts,
            TodayEvents = events.Count,
            RealtimeMessages = events.Count,
            Trend = BuildTrend(events.Count),
            Plazas = plazaItems,
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
