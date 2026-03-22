using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Application.Services;

public sealed class LaneStatusQueryService : ILaneStatusQueryService
{
    private readonly IPlazaContextService _plazaContextService;
    private readonly ILaneStatusSnapshotStore _snapshotStore;

    public LaneStatusQueryService(IPlazaContextService plazaContextService, ILaneStatusSnapshotStore snapshotStore)
    {
        _plazaContextService = plazaContextService;
        _snapshotStore = snapshotStore;
    }

    public Task<IReadOnlyCollection<PlazaLaneSnapshotDto>> GetPlazaLaneSnapshotsAsync(CancellationToken cancellationToken = default)
    {
        var snapshots = _snapshotStore.GetAll().ToDictionary(item => item.PlazaId, StringComparer.OrdinalIgnoreCase);
        var plazas = _plazaContextService.GetPlazas();

        var result = plazas.Select(plaza =>
        {
            if (snapshots.TryGetValue(plaza.Id ?? string.Empty, out var snapshot))
            {
                snapshot.PlazaName = plaza.StationName ?? snapshot.PlazaName;
                snapshot.LanesTotal = Math.Max(snapshot.LanesTotal, plaza.Lanes.Count);
                return snapshot;
            }

            return new PlazaLaneSnapshotDto
            {
                PlazaId = plaza.Id ?? string.Empty,
                PlazaName = plaza.StationName ?? plaza.Id ?? string.Empty,
                LanesOnline = 0,
                LanesTotal = plaza.Lanes.Count,
                Alerts = 0,
                Lanes = plaza.Lanes.Select(lane => new LaneStatusSnapshotDto
                {
                    PlazaId = plaza.Id ?? string.Empty,
                    PlazaName = plaza.StationName ?? plaza.Id ?? string.Empty,
                    LaneId = lane.LaneId ?? lane.Id ?? lane.LaneNo ?? string.Empty,
                    LaneNo = lane.LaneNo ?? string.Empty,
                    Status = "offline",
                    HasWarning = false,
                    LastMessage = "等待实时状态接入",
                    LastHeartbeat = "未接入"
                }).ToList()
            };
        }).ToArray();

        return Task.FromResult<IReadOnlyCollection<PlazaLaneSnapshotDto>>(result);
    }
}