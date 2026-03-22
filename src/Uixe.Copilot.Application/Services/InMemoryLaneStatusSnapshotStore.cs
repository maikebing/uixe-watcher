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
            var (plazaSnapshot, existing) = GetOrCreateLane(plazaId, laneNo, plaza, lane);

            var hasWarning = !status.NetworkStatus || !status.CameraStatus || !status.PrinterStatus || !status.LanGanStatus || !status.BaoJingStatus;
            var isOnline = status.NetworkStatus || status.CameraStatus || status.PrinterStatus;

            existing.PlazaId = plazaId;
            existing.PlazaName = plaza?.StationName ?? plazaSnapshot.PlazaName;
            existing.LaneId = lane?.LaneId ?? existing.LaneId;
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
            existing.IsLost = false;

            RefreshPlaza(plazaSnapshot);
        }
    }

    public void MarkLaneLost(string plazaId, string laneNo, PlazaInfo? plaza, LaneInfo? lane)
    {
        lock (_syncRoot)
        {
            var (plazaSnapshot, existing) = GetOrCreateLane(plazaId, laneNo, plaza, lane);
            existing.Status = "offline";
            existing.IsLost = true;
            existing.HasWarning = true;
            existing.LastMessage = "车道掉线";
            existing.LastHeartbeat = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            existing.Alerts.Insert(0, new LaneAlertSnapshotDto
            {
                Category = "lane-lost",
                Title = "车道掉线",
                Content = $"{existing.LaneNo} 车道心跳中断",
                Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            });
            TrimAlerts(existing);
            RefreshPlaza(plazaSnapshot);
        }
    }

    public void AddMessage(string plazaId, LaneMessageDto message, PlazaInfo? plaza, LaneInfo? lane)
    {
        lock (_syncRoot)
        {
            var laneNo = lane?.LaneNo ?? message.LaneNo ?? string.Empty;
            var (plazaSnapshot, existing) = GetOrCreateLane(plazaId, laneNo, plaza, lane);
            var content = message.PromptMsg ?? message.Exception ?? message.DevStatus ?? message.MsgType ?? "收到车道消息";
            existing.LastMessage = content;
            existing.LastMessageType = message.MsgType;
            existing.LastMessageTime = message.OccDateTime;
            existing.Messages.Insert(0, new LaneMessageSnapshotDto
            {
                Type = message.MsgType ?? "message",
                Content = content,
                Time = message.OccDateTime == default ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : message.OccDateTime.ToString("yyyy-MM-dd HH:mm:ss")
            });
            TrimMessages(existing);
            RefreshPlaza(plazaSnapshot);
        }
    }

    public void AddOverloadAlert(string plazaId, OverloadWarningDto warning, PlazaInfo? plaza, LaneInfo? lane)
    {
        lock (_syncRoot)
        {
            var laneNo = lane?.LaneNo ?? ExtractLaneNo(warning.Title, warning.Context);
            var (plazaSnapshot, existing) = GetOrCreateLane(plazaId, laneNo, plaza, lane);
            existing.HasWarning = true;
            existing.Status = "warning";
            existing.Alerts.Insert(0, new LaneAlertSnapshotDto
            {
                Category = "overload",
                Title = warning.Title ?? "超限告警",
                Content = warning.Context ?? warning.Id ?? "收到超限告警",
                Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            });
            existing.LastMessage = warning.Context ?? warning.Title ?? existing.LastMessage;
            TrimAlerts(existing);
            RefreshPlaza(plazaSnapshot);
        }
    }

    public void AddLaneSpecial(string plazaId, LaneSpecialDto message, PlazaInfo? plaza, LaneInfo? lane)
    {
        lock (_syncRoot)
        {
            var laneNo = lane?.LaneNo ?? ExtractLaneNo(message.LaneId, message.Context);
            var (plazaSnapshot, existing) = GetOrCreateLane(plazaId, laneNo, plaza, lane);
            existing.HasWarning = true;
            existing.Status = "warning";
            existing.Alerts.Insert(0, new LaneAlertSnapshotDto
            {
                Category = "lane-special",
                Title = message.Title ?? "车道特情",
                Content = message.Context ?? $"特情代码：{message.SpecialCode}",
                Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            });
            existing.LastMessage = message.Context ?? message.Title ?? existing.LastMessage;
            TrimAlerts(existing);
            RefreshPlaza(plazaSnapshot);
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
                        ,IsLost = lane.IsLost
                        ,LastMessageType = lane.LastMessageType
                        ,LastMessageTime = lane.LastMessageTime
                        ,Messages = lane.Messages.Select(message => new LaneMessageSnapshotDto
                        {
                            Type = message.Type,
                            Content = message.Content,
                            Time = message.Time
                        }).ToList()
                        ,Alerts = lane.Alerts.Select(alert => new LaneAlertSnapshotDto
                        {
                            Category = alert.Category,
                            Title = alert.Title,
                            Content = alert.Content,
                            Time = alert.Time
                        }).ToList()
                    }).ToList()
                })
                .ToArray();
        }
    }

    private (PlazaLaneSnapshotDto plazaSnapshot, LaneStatusSnapshotDto laneSnapshot) GetOrCreateLane(string plazaId, string laneNo, PlazaInfo? plaza, LaneInfo? lane)
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
            existing = new LaneStatusSnapshotDto
            {
                PlazaId = plazaId,
                PlazaName = plazaSnapshot.PlazaName,
                LaneId = lane?.LaneId ?? laneKey,
                LaneNo = lane?.LaneNo ?? laneNo,
                Status = "offline",
                LastMessage = "等待实时状态接入",
                LastHeartbeat = "未接入"
            };
            plazaSnapshot.Lanes.Add(existing);
        }

        existing.PlazaId = plazaId;
        existing.PlazaName = plazaSnapshot.PlazaName;
        existing.LaneId = lane?.LaneId ?? existing.LaneId;
        existing.LaneNo = lane?.LaneNo ?? existing.LaneNo;
        return (plazaSnapshot, existing);
    }

    private static void RefreshPlaza(PlazaLaneSnapshotDto plazaSnapshot)
    {
        plazaSnapshot.Lanes = plazaSnapshot.Lanes
            .OrderBy(item => item.LaneNo, StringComparer.OrdinalIgnoreCase)
            .ToList();
        plazaSnapshot.LanesOnline = plazaSnapshot.Lanes.Count(item => string.Equals(item.Status, "online", StringComparison.OrdinalIgnoreCase));
        plazaSnapshot.Alerts = plazaSnapshot.Lanes.Count(item => item.HasWarning || item.IsLost || item.Alerts.Count > 0);
        plazaSnapshot.LanesTotal = Math.Max(plazaSnapshot.LanesTotal, plazaSnapshot.Lanes.Count);
    }

    private static void TrimMessages(LaneStatusSnapshotDto lane)
    {
        if (lane.Messages.Count > 10)
        {
            lane.Messages = lane.Messages.Take(10).ToList();
        }
    }

    private static void TrimAlerts(LaneStatusSnapshotDto lane)
    {
        if (lane.Alerts.Count > 10)
        {
            lane.Alerts = lane.Alerts.Take(10).ToList();
        }
    }

    private static string ExtractLaneNo(params string?[] values)
    {
        foreach (var value in values)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                continue;
            }

            var digits = new string(value.Where(char.IsDigit).ToArray());
            if (digits.Length >= 3)
            {
                return digits[^3..];
            }
        }

        return string.Empty;
    }
}