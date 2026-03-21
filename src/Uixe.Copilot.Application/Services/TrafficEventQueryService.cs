using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Application.Services;

public sealed class TrafficEventQueryService : ITrafficEventQueryService
{
    public Task<TrafficEventOverviewDto> GetOverviewAsync(CancellationToken cancellationToken = default)
    {
        var overview = new TrafficEventOverviewDto
        {
            OnlineStations = 24,
            TotalStations = 28,
            ActiveAlerts = 6,
            TodayEvents = 132,
            RealtimeMessages = 18,
            Trend = new List<int> { 32, 45, 41, 66, 58, 75, 89 },
            Plazas = new List<TrafficEventPlazaStatusDto>
            {
                new() { Id = "6500256", Name = "城北收费站", Status = "online", LanesOnline = 8, LanesTotal = 10, Alerts = 2 },
                new() { Id = "6500257", Name = "高新收费站", Status = "warning", LanesOnline = 6, LanesTotal = 8, Alerts = 3 },
                new() { Id = "6500258", Name = "机场收费站", Status = "online", LanesOnline = 12, LanesTotal = 12, Alerts = 0 }
            },
            Events = new List<TrafficEventListItemDto>
            {
                new() { Id = "evt-1", Title = "排队溢出", PlazaName = "城北收费站", LaneNo = "103", Level = "high", Time = "12:18:47", Status = "待处理" },
                new() { Id = "evt-2", Title = "发票信息确认", PlazaName = "高新收费站", LaneNo = "205", Level = "medium", Time = "12:22:11", Status = "处理中" },
                new() { Id = "evt-3", Title = "入口信息确认", PlazaName = "机场收费站", LaneNo = "008", Level = "low", Time = "12:25:33", Status = "已完成" }
            }
        };

        return Task.FromResult(overview);
    }

    public async Task<TrafficEventListItemDto?> GetByIdAsync(string eventId, CancellationToken cancellationToken = default)
    {
        var overview = await GetOverviewAsync(cancellationToken);
        return overview.Events.FirstOrDefault(x => string.Equals(x.Id, eventId, StringComparison.OrdinalIgnoreCase));
    }
}
