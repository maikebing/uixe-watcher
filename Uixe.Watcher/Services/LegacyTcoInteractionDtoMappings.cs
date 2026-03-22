using System.Globalization;
using Uixe.Copilot.Contracts.Dtos;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;

namespace Uixe.Watcher.Services;

internal static class LegacyTcoInteractionDtoMappings
{
    public static MsgWeightTCOCALL ToLegacyMsgWeightTcoCall(this TcoWeightMessageDto source)
    {
        return new MsgWeightTCOCALL
        {
            Head = source.Head.ToLegacyHead(),
            SubHead = source.SubHead.ToLegacySubHead(),
            MsgTcoTran = source.Tran.ToLegacyMsgTcoTran(),
            WatcherID = (WATCHER_TYPE)source.WatcherId,
            DlgType = (DlgType)source.DialogType,
            WeightFunctions = source.WeightFunctions,
            FareFormula = source.FareFormula,
            TimeOut = source.TimeOut
        };
    }

    public static TCOCall ToLegacyTcoCall(this TcoConfirmRequestDto source)
    {
        return new TCOCall
        {
            Head = source.Head.ToLegacyHead(),
            SubHead = source.SubHead.ToLegacySubHead(),
            MsgTcoTran = source.Tran.ToLegacyMsgTcoTran(),
            WatcherID = (WATCHER_TYPE)source.WatcherId,
            DlgType = (DlgType)source.DialogType,
            TimeOut = source.TimeOut
        };
    }

    private static Head ToLegacyHead(this MessageHeadDto? source)
    {
        return new Head
        {
            NetNo = source?.NetNo,
            PlazaNo = source?.PlazaNo,
            LaneID = source?.LaneId,
            DDHM = source?.Ddhm,
            LaneType = source?.LaneType ?? 0,
            MsgLen = source?.MsgLen,
            MsgType = source?.MsgType,
            MsgVersion = source?.MsgVersion,
            Reserved = source?.Reserved
        };
    }

    private static SubHead ToLegacySubHead(this MessageSubHeadDto? source)
    {
        return new SubHead
        {
            LaneMode = source?.LaneMode ?? 0,
            CETC = source?.CETC ?? 0,
            StaffID = source?.StaffId,
            StaffName = source?.StaffName,
            JobNo = source?.JobNo,
            SquadID = source?.SquadId ?? 0,
            ShiftNo = source?.ShiftNo ?? 0
        };
    }

    private static MsgTcoTran ToLegacyMsgTcoTran(this TcoTranDto? source)
    {
        return new MsgTcoTran
        {
            TransNO = source?.TransNo,
            WeightType = source?.WeightType,
            CarClass = source?.CarClass,
            ExitVehiKind = source?.ExitVehiKind,
            Distance = source?.Distance,
            ExitPlate = source?.ExitPlate,
            InputPlate = source?.InputPlate,
            DetectAxleCount = source?.DetectAxleCount,
            DetectWeightTotal = source?.DetectWeightTotal,
            Speed = source?.Speed,
            OverloadWeight = source?.OverloadWeight,
            OverloadReason = source?.OverloadReason,
            FWeightTotal = source?.FWeightTotal,
            Detail = source?.Detail,
            WeightLimit = source?.WeightLimit,
            EntryNetNo = source?.EntryNetNo,
            EntryPlazaNo = source?.EntryPlazaNo,
            EntryCarType = source?.EntryCarType,
            VehicleType = source?.VehicleType ?? 0,
            VehicleTypeChinese = source?.VehicleTypeChinese,
            EntryVehiTKind = source?.EntryVehiTKind,
            EntryPlate = source?.EntryPlate,
            EntryDHM = source?.EntryDhm,
            DifPlaza = source?.DifPlaza,
            DifPlate = source?.DifPlate,
            DifClass = source?.DifClass,
            DifType = source?.DifType,
            UCar = source?.UCar,
            TimeoutCar = source?.TimeoutCar,
            CardNo = source?.CardNo,
            EntryLaneID = source?.EntryLaneId,
            EntryStationID = source?.EntryStationId,
            EntryStationName = source?.EntryStationName
        };
    }
}