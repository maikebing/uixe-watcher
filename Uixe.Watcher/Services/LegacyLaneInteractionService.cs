using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.WinForms;

namespace Uixe.Watcher.Services;

public sealed class LegacyLaneInteractionService : ILegacyLaneInteractionService
{
    public Task<bool> ShowBulkTransportAsync(object plazaHost, string laneId, BulkTransportDto dto, CancellationToken cancellationToken = default)
    {
        if (plazaHost is not frmPlaza frm || dto.Head is null)
        {
            return Task.FromResult(false);
        }

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

        return Task.FromResult(true);
    }

    public Task<bool> ShowBillInfoAsync(object plazaHost, string laneId, BillInfoRequestDto dto, CancellationToken cancellationToken = default)
    {
        if (plazaHost is not frmPlaza frm || dto.Head is null)
        {
            return Task.FromResult(false);
        }

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

        return Task.FromResult(true);
    }

    public Task<bool> ShowConfirmEnInfoAsync(object plazaHost, ConfirmEnInfoDto dto, CancellationToken cancellationToken = default)
    {
        if (plazaHost is not frmPlaza frm)
        {
            return Task.FromResult(false);
        }

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

        return Task.FromResult(true);
    }
}
