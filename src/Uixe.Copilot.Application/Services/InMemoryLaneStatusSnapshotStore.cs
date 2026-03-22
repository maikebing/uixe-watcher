using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Application.Services;

public sealed class InMemoryLaneStatusSnapshotStore : ILaneStatusSnapshotStore
{
    private readonly object _syncRoot = new();
    private readonly Dictionary<string, PlazaLaneSnapshotDto> _plazas = new(StringComparer.OrdinalIgnoreCase);

    public void Upsert(string plazaId, string laneNo, LaneStatusDto status, PlazaInfo? plaza, LaneInfo? lane)
    {
        lock (_syncRoot)
        {
            if (!_plazas.TryGetValue(plazaId, out var plazaSnapshot))
            {
                plazaSnapshot = new PlazaLaneSnapshotDto
                {
                    PlazaId = plazaId,
                    PlazaName = plaza?.StationName ?? plazaId
                };
                _plazas[plazaId] = plazaSnapshot;
            }

            plazaSnapshot.PlazaName = plaza?.StationName ?? plazaSnapshot.PlazaName;
            plazaSnapshot.LanesTotal = Math.Max(plaza?.Lanes?.Count ?? 0, plazaSnapshot.LanesTotal);

            var laneKey = lane?.LaneId ?? laneNo;
            var existing = plazaSnapshot.Lanes.FirstOrDefault(item => string.Equals(item.LaneId, laneKey, StringComparison.OrdinalIgnoreCase))
                ?? plazaSnapshot.Lanes.FirstOrDefault(item => string.Equals(item.LaneNo, laneNo, StringComparison.OrdinalIgnoreCase));

            if (existing is null)
            {
                existing = new LaneStatusSnapshotDto();
                plazaSnapshot.Lanes.Add(existing);
            }

            var hasWarning = !status.NetworkStatus || !status.CameraStatus || !status.PrinterStatus || !status.LanGanStatus || !status.BaoJingStatus;
            var isOnline = status.NetworkStatus || status.CameraStatus || status.PrinterStatus;

            existing.PlazaId = plazaId;
            existing.PlazaName = plaza?.StationName ?? plazaSnapshot.PlazaName;
            existing.LaneId = lane?.LaneId ?? laneKey;
            existing.LaneNo = lane?.LaneNo ?? laneNo;
            existing.Status = hasWarning ? "warning" : isOnline ? "online" : "offline";
            existing.HasWarning = hasWarning;
            existing.LastMessage = string.IsNullOrWhiteSpace(status.ClientMsg) ? (hasWarning ? "设备状态异常" : "车道心跳正常") : status.ClientMsg!;
            existing.LastHeartbeat = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            existing.CollectorName = status.CollName;
            existing.WorkMode = status.WrokMode;
            existing.VideoRtsp = status.VideoRtsp;
            existing.UpdatedAt = DateTime.Now;
            existing.NetworkStatus = status.NetworkStatus;
            existing.CameraStatus = status.CameraStatus;
            existing.PrinterStatus = status.PrinterStatus;
            existing.BarrierStatus = status.LanGanStatus;

            plazaSnapshot.Lanes = plazaSnapshot.Lanes
                .OrderBy(item => item.LaneNo, StringComparer.OrdinalIgnoreCase)
                .ToList();
            plazaSnapshot.LanesOnline = plazaSnapshot.Lanes.Count(item => string.Equals(item.Status, "online", StringComparison.OrdinalIgnoreCase));
            plazaSnapshot.Alerts = plazaSnapshot.Lanes.Count(item => item.HasWarning);
            plazaSnapshot.LanesTotal = Math.Max(plazaSnapshot.LanesTotal, plazaSnapshot.Lanes.Count);
        }
    }

    public IReadOnlyCollection<PlazaLaneSnapshotDto> GetAll()
    {
        lock (_syncRoot)
        {
            return _plazas.Values
                .Select(item => new PlazaLaneSnapshotDto
                {
                    PlazaId = item.PlazaId,
                    PlazaName = item.PlazaName,
                    LanesOnline = item.LanesOnline,
                    LanesTotal = item.LanesTotal,
                    Alerts = item.Alerts,
                    Lanes = item.Lanes.Select(lane => new LaneStatusSnapshotDto
                    {
                        PlazaId = lane.PlazaId,
                        PlazaName = lane.PlazaName,
                        LaneId = lane.LaneId,
                        LaneNo = lane.LaneNo,
                        Status = lane.Status,
                        HasWarning = lane.HasWarning,
                        LastMessage = lane.LastMessage,
                        LastHeartbeat = lane.LastHeartbeat,
                        CollectorName = lane.CollectorName,
                        WorkMode = lane.WorkMode,
                        VideoRtsp = lane.VideoRtsp,
                        UpdatedAt = lane.UpdatedAt,
                        NetworkStatus = lane.NetworkStatus,
                        CameraStatus = lane.CameraStatus,
                        PrinterStatus = lane.PrinterStatus,
                        BarrierStatus = lane.BarrierStatus
                    }).ToList()
                })
                .ToArray();
        }
    }
}