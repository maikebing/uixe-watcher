using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Contracts.Responses;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;
using Uixe.Watcher.Ring;

namespace Uixe.Watcher.Services;

public sealed class LegacyPlazaUiBridge : ILegacyPlazaUiBridge
{
    private readonly IPlazaContextService _plazaContextService;

    public LegacyPlazaUiBridge(IPlazaContextService plazaContextService)
    {
        _plazaContextService = plazaContextService;
    }

    public Task<ApiResult> ShowLaneStatusAsync(string plazaId, string laneNo, object status, CancellationToken cancellationToken = default)
        => ExecuteOnPlazaAsync(plazaId, frm => frm.ShowLaneInfor(plazaId, laneNo, (LaneStatus)status));

    public Task<ApiResult> ShowMessageAsync(string plazaId, object message, CancellationToken cancellationToken = default)
        => ExecuteOnPlazaAsync(plazaId, frm => frm.ShowMessageView((MsgInfo)message));

    public async Task<ApiResult> ShowOverloadAlarmAsync(string plazaId, string title, string context, bool playSpeech, CancellationToken cancellationToken = default)
    {
        var frm = _plazaContextService.GetPlazaHost(plazaId) as frmPlaza;
        if (frm == null)
        {
            return new ApiResult(ApiCode.NotFound, $"没有找到{plazaId}的收费站ID");
        }

        await Task.Run(() => frm.Invoke(() => frm.Alert(title, context)), cancellationToken);
        return new ApiResult(ApiCode.OK, "OK");
    }

    public async Task<ApiResult> ShowLaneSpecialAsync(string plazaId, object message, CancellationToken cancellationToken = default)
    {
        var frm = _plazaContextService.GetPlazaHost(plazaId) as frmPlaza;
        if (frm == null)
        {
            return new ApiResult(ApiCode.NotFound, $"没有找到{plazaId}的收费站ID");
        }

        var msg = (Lanespecial)message;
        await Task.Run(() => frm.Invoke(() => frm.Alert(msg.Title, msg.Context)), cancellationToken);
        return new ApiResult(ApiCode.OK, "OK");
    }

    public void PlayAlertRing()
    {
        Task.Run(PlayUitls.PlayRing);
    }

    public object? GetTrafficEventDisplayHost(string plazaId)
        => _plazaContextService.GetPlazaHost(plazaId) as frmPlaza;

    public TrafficEventTargetResolutionResult? TryResolveTrafficEventTarget(TrafficEventPushRequestDto request)
    {
        var plazas = _plazaContextService.GetPlazas();
        if (request == null || string.IsNullOrWhiteSpace(request.LaneNo) || plazas.Count == 0)
        {
            return null;
        }

        foreach (var currentPlaza in plazas)
        {
            var currentLane = currentPlaza?.Lanes?.FirstOrDefault(item => IsLaneMatch(item?.LaneNo, item?.LaneId, request.LaneNo));
            if (currentLane == null)
            {
                continue;
            }

            var currentForm = _plazaContextService.GetPlazaHost(currentPlaza.Id) as frmPlaza;
            if (currentForm == null)
            {
                continue;
            }

            return new TrafficEventTargetResolutionResult
            {
                DisplayHost = currentForm,
                Plaza = new T_Plaza
                {
                    Id = currentPlaza.Id,
                    StationId = currentPlaza.StationId,
                    StationName = currentPlaza.StationName,
                    Lanes = currentPlaza.Lanes?.Select(x => new T_Lane { Id = x.Id, LaneId = x.LaneId, LaneNo = x.LaneNo }).ToList()
                },
                Lane = new T_Lane { Id = currentLane.Id, LaneId = currentLane.LaneId, LaneNo = currentLane.LaneNo },
                FormRequest = MapRequest(request)
            };
        }

        return null;
    }

    private Task<ApiResult> ExecuteOnPlazaAsync(string plazaId, Action<frmPlaza> action)
    {
        var frm = _plazaContextService.GetPlazaHost(plazaId) as frmPlaza;
        if (frm == null)
        {
            return Task.FromResult(new ApiResult(ApiCode.NotFound, $"没有找到{plazaId}的收费站ID"));
        }

        return Task.Run(() =>
        {
            action(frm);
            return new ApiResult(ApiCode.OK, "OK");
        });
    }

    private static bool IsLaneMatch(string laneNoValue, string laneIdValue, string laneNo)
    {
        if (string.IsNullOrWhiteSpace(laneNo))
        {
            return false;
        }

        return IsLaneTokenMatch(laneNo, laneNoValue)
            || IsLaneTokenMatch(laneNo, laneIdValue);
    }

    private static bool IsLaneTokenMatch(string left, string right)
    {
        var leftValue = NormalizeLaneToken(left);
        var rightValue = NormalizeLaneToken(right);
        if (string.IsNullOrEmpty(leftValue) || string.IsNullOrEmpty(rightValue))
        {
            return false;
        }

        return leftValue == rightValue || leftValue.EndsWith(rightValue) || rightValue.EndsWith(leftValue);
    }

    private static string NormalizeLaneToken(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        return new string(value.Where(c => !char.IsWhiteSpace(c) && c != '-' && c != '_').ToArray()).ToUpperInvariant();
    }

    private static TrafficEventPushRequest MapRequest(TrafficEventPushRequestDto request)
    {
        return new TrafficEventPushRequest
        {
            RecordId = request.RecordId,
            EventType = request.EventType,
            LaneNo = request.LaneNo,
            CapTime = request.CapTime,
            StartTime = request.StartTime,
            Period = request.Period,
            PeriodByMili = request.PeriodByMili,
            MaxQueueLen = request.MaxQueueLen,
            ImageList = request.ImageList,
            VideoList = request.VideoList
        };
    }
}