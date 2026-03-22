using System;
using System.Linq;
using System.Windows.Forms;
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

    public Task<ApiResult> ShowLaneStatusAsync(string plazaId, string laneNo, LaneStatusDto status, CancellationToken cancellationToken = default)
        => ExecuteOnPlazaAsync(plazaId, frm => frm.ShowLaneInfor(plazaId, laneNo, new LaneStatus
        {
            LaneNo = status.LaneNo,
            CollName = status.CollName,
            CollNo = status.CollNo,
            ClientMsg = status.ClientMsg,
            CarType = status.CarType,
            Money = status.Money,
            CarKind = status.CarKind,
            WrokMode = status.WrokMode,
            JobBeginTime = status.JobBeginTime,
            YuPengDengStatus = status.YuPengDengStatus,
            JiaoTongDengStatus = status.JiaoTongDengStatus,
            LanGanStatus = status.LanGanStatus,
            Coil1Status = status.Coil1Status,
            Coil2Status = status.Coil2Status,
            Coil3Status = status.Coil3Status,
            Coil4Status = status.Coil4Status,
            PrinterStatus = status.PrinterStatus,
            NetworkStatus = status.NetworkStatus,
            RSUStatus = status.RSUStatus,
            ReaderStatus = status.ReaderStatus,
            WeightStatus = status.WeightStatus,
            VPRStatus = status.VPRStatus,
            CameraStatus = status.CameraStatus,
            YellowStatus = status.YellowStatus,
            QRPayStatus = status.QRPayStatus,
            BaoJingStatus = status.BaoJingStatus,
            LWDStatus = status.LWDStatus,
            CarBoxID = status.CarBoxID,
            CarBoxNow = status.CarBoxNow,
            CarBoxMax = status.CarBoxMax,
            terminalId = status.TerminalId,
            VideoRtsp = status.VideoRtsp
        }));

    public Task<ApiResult> ShowLaneLostAsync(string plazaId, string laneNo, CancellationToken cancellationToken = default)
        => ExecuteOnPlazaAsync(plazaId, frm => frm.ShowLaneLost(plazaId, laneNo));

    public Task<ApiResult> ShowMessageAsync(string plazaId, LaneMessageDto message, CancellationToken cancellationToken = default)
        => ExecuteOnPlazaAsync(plazaId, frm => frm.ShowMessageView(new MsgInfo
        {
            LaneNo = message.LaneNo,
            MsgType = message.MsgType,
            OccDateTime = message.OccDateTime,
            CollNo = message.CollNo,
            CarKind = message.CarKind,
            CarType = message.CarType,
            PayType = message.PayType,
            Cash = message.Cash,
            Receipt = message.Receipt,
            Exception = message.Exception,
            Peccancy = message.Peccancy,
            DevStatus = message.DevStatus,
            PromptMsg = message.PromptMsg,
            PlazaId = message.PlazaId
        }));

    public Task<ApiResult> ShowBulkTransportAsync(string plazaId, BulkTransportDto dto, CancellationToken cancellationToken = default)
        => ExecuteOnPlazaAsync(plazaId, frm =>
        {
            if (dto.Head is null)
            {
                return;
            }

            string laneId = $"650{dto.Head.NetNo}{dto.Head.PlazaNo}{dto.Head.LaneId}";
            frm.ShowBulktrans(laneId, new BulklyDto
            {
                Head = new Head
                {
                    NetNo = dto.Head.NetNo,
                    PlazaNo = dto.Head.PlazaNo,
                    LaneID = dto.Head.LaneId,
                    DDHM = dto.Head.Ddhm,
                    LaneType = dto.Head.LaneType,
                    MsgLen = dto.Head.MsgLen,
                    MsgType = dto.Head.MsgType,
                    MsgVersion = dto.Head.MsgVersion,
                    Reserved = dto.Head.Reserved
                },
                SubHead = dto.SubHead is null ? null : new SubHead
                {
                    LaneMode = dto.SubHead.LaneMode,
                    CETC = dto.SubHead.CETC,
                    StaffID = dto.SubHead.StaffId,
                    StaffName = dto.SubHead.StaffName,
                    JobNo = dto.SubHead.JobNo,
                    SquadID = dto.SubHead.SquadId,
                    ShiftNo = dto.SubHead.ShiftNo
                },
                VehId = dto.VehId,
                VehColor = dto.VehColor,
                Alex = dto.Alex,
                Weight = dto.Weight,
                IsValid = dto.IsValid,
                Title = dto.Title,
                LARGEWOODS = dto.LargeWoods is null ? null : new LARGEWOODS
                {
                    LINCENSE = dto.LargeWoods.Lincense,
                    PLATE = dto.LargeWoods.Plate,
                    EN_STATION_ID = dto.LargeWoods.EnStationId,
                    EX_STATION_ID = dto.LargeWoods.ExStationId,
                    START_PASS_DATE = dto.LargeWoods.StartPassDate,
                    END_PASS_DATE = dto.LargeWoods.EndPassDate,
                    CARS_TOTAL_WEIGHT = dto.LargeWoods.CarsTotalWeight,
                    CAR_LENGTH = dto.LargeWoods.CarLength,
                    CAR_WIDTH = dto.LargeWoods.CarWidth,
                    CAR_HEIGHT = dto.LargeWoods.CarHeight,
                    CAR_AXLENUM = dto.LargeWoods.CarAxleNum,
                    VERSION = dto.LargeWoods.Version
                }
            });
        });

    public Task<ApiResult> ShowBillInfoAsync(string plazaId, BillInfoRequestDto dto, CancellationToken cancellationToken = default)
        => ExecuteOnPlazaAsync(plazaId, frm =>
        {
            if (dto.Head is null)
            {
                return;
            }

            string laneId = $"650{dto.Head.NetNo}{dto.Head.PlazaNo}{dto.Head.LaneId}";
            frm.ShowBillInfo(laneId, new BillInfoDto
            {
                Head = new Head
                {
                    NetNo = dto.Head.NetNo,
                    PlazaNo = dto.Head.PlazaNo,
                    LaneID = dto.Head.LaneId,
                    DDHM = dto.Head.Ddhm,
                    LaneType = dto.Head.LaneType,
                    MsgLen = dto.Head.MsgLen,
                    MsgType = dto.Head.MsgType,
                    MsgVersion = dto.Head.MsgVersion,
                    Reserved = dto.Head.Reserved
                },
                SubHead = dto.SubHead is null ? null : new SubHead
                {
                    LaneMode = dto.SubHead.LaneMode,
                    CETC = dto.SubHead.CETC,
                    StaffID = dto.SubHead.StaffId,
                    StaffName = dto.SubHead.StaffName,
                    JobNo = dto.SubHead.JobNo,
                    SquadID = dto.SubHead.SquadId,
                    ShiftNo = dto.SubHead.ShiftNo
                },
                billCode = dto.BillCode,
                billNumber = dto.BillNumber
            });
        });

    public Task<ApiResult> ShowConfirmEnInfoAsync(string plazaId, ConfirmEnInfoDto dto, CancellationToken cancellationToken = default)
        => ExecuteOnPlazaAsync(plazaId, frm =>
        {
            frm.ShowConfirmEnInfo(new ConfirmEnInfo
            {
                laneId = dto.LaneId,
                genTime = dto.GenTime,
                vehicleId = dto.VehicleId,
                vehicleType = dto.VehicleType,
                resCount = dto.ResCount,
                retQuery = dto.RetQuery,
                code = dto.Code,
                msg = dto.Msg,
                enStations = dto.EnStations.Select(x => new EnStations
                {
                    cardId = x.CardId,
                    enStationId = x.EnStationId,
                    enDateTime = x.EnDateTime,
                    enTollLaneId = x.EnTollLaneId,
                    mediaNo = x.MediaNo,
                    mediaType = x.MediaType,
                    resultVoucher = x.ResultVoucher
                }).ToList()
            });
        });

    public async Task<ApiResult> ShowOverloadAlarmAsync(string plazaId, OverloadWarningDto warning, bool playSpeech, CancellationToken cancellationToken = default)
    {
        var frm = _plazaContextService.GetPlazaHost(plazaId) as frmPlaza;
        if (frm == null)
        {
            return new ApiResult(ApiCode.NotFound, $"没有找到{plazaId}的收费站ID");
        }

        await Task.Run(() => frm.Invoke(() => frm.Alert(warning.Title ?? string.Empty, warning.Context ?? string.Empty)), cancellationToken);
        return new ApiResult(ApiCode.OK, "OK");
    }

    public async Task<ApiResult> ShowLaneSpecialAsync(string plazaId, LaneSpecialDto message, CancellationToken cancellationToken = default)
    {
        var frm = _plazaContextService.GetPlazaHost(plazaId) as frmPlaza;
        if (frm == null)
        {
            return new ApiResult(ApiCode.NotFound, $"没有找到{plazaId}的收费站ID");
        }

        await Task.Run(() => frm.Invoke(() => frm.Alert(message.Title ?? string.Empty, message.Context ?? string.Empty)), cancellationToken);
        return new ApiResult(ApiCode.OK, "OK");
    }

    public void PlayAlertRing()
    {
        Task.Run(PlayUitls.PlayRing);
    }

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
                DisplayAction = cancellationToken => ShowTrafficEventAsync(currentForm, currentPlaza, currentLane, request, cancellationToken),
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

    private static Task ShowTrafficEventAsync(frmPlaza form, PlazaInfo currentPlaza, LaneInfo currentLane, TrafficEventPushRequestDto request, CancellationToken cancellationToken)
    {
        TaskCompletionSource<bool> taskCompletionSource = new(TaskCreationOptions.RunContinuationsAsynchronously);

        try
        {
            if (form.IsDisposed || !form.IsHandleCreated)
            {
                taskCompletionSource.SetResult(true);
                return taskCompletionSource.Task;
            }

            form.BeginInvoke((MethodInvoker)delegate
            {
                try
                {
                    if (form.IsDisposed || !form.IsHandleCreated)
                    {
                        taskCompletionSource.TrySetResult(true);
                        return;
                    }

                    var trafficForm = form.ShowTrafficEvent(
                        new T_Plaza
                        {
                            Id = currentPlaza.Id,
                            StationId = currentPlaza.StationId,
                            StationName = currentPlaza.StationName,
                            Lanes = currentPlaza.Lanes?.Select(x => new T_Lane { Id = x.Id, LaneId = x.LaneId, LaneNo = x.LaneNo }).ToList()
                        },
                        new T_Lane
                        {
                            Id = currentLane.Id,
                            LaneId = currentLane.LaneId,
                            LaneNo = currentLane.LaneNo
                        },
                        MapRequest(request));

                    if (trafficForm == null || trafficForm.IsDisposed)
                    {
                        taskCompletionSource.TrySetResult(true);
                        return;
                    }

                    FormClosedEventHandler? closedHandler = null;
                    closedHandler = delegate
                    {
                        trafficForm.FormClosed -= closedHandler;
                        taskCompletionSource.TrySetResult(true);
                    };
                    trafficForm.FormClosed += closedHandler;
                }
                catch (Exception ex)
                {
                    taskCompletionSource.TrySetException(ex);
                }
            });
        }
        catch (Exception ex)
        {
            taskCompletionSource.TrySetException(ex);
        }

        if (cancellationToken.CanBeCanceled)
        {
            cancellationToken.Register(() => taskCompletionSource.TrySetCanceled(cancellationToken));
        }

        return taskCompletionSource.Task;
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