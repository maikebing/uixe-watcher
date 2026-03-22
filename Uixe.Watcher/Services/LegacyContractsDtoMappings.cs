using System.Linq;
using Uixe.Copilot.Contracts.Dtos;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;

namespace Uixe.Watcher.Services;

public static class LegacyContractsDtoMappings
{
    public static LaneStatusDto ToLaneStatusDto(this LaneStatus source)
    {
        return new LaneStatusDto
        {
            LaneNo = source.LaneNo,
            CollName = source.CollName,
            CollNo = source.CollNo,
            ClientMsg = source.ClientMsg,
            CarType = source.CarType,
            Money = source.Money,
            CarKind = source.CarKind,
            WrokMode = source.WrokMode,
            JobBeginTime = source.JobBeginTime,
            YuPengDengStatus = source.YuPengDengStatus,
            JiaoTongDengStatus = source.JiaoTongDengStatus,
            LanGanStatus = source.LanGanStatus,
            Coil1Status = source.Coil1Status,
            Coil2Status = source.Coil2Status,
            Coil3Status = source.Coil3Status,
            Coil4Status = source.Coil4Status,
            PrinterStatus = source.PrinterStatus,
            NetworkStatus = source.NetworkStatus,
            RSUStatus = source.RSUStatus,
            ReaderStatus = source.ReaderStatus,
            WeightStatus = source.WeightStatus,
            VPRStatus = source.VPRStatus,
            CameraStatus = source.CameraStatus,
            YellowStatus = source.YellowStatus,
            QRPayStatus = source.QRPayStatus,
            BaoJingStatus = source.BaoJingStatus,
            LWDStatus = source.LWDStatus,
            CarBoxID = source.CarBoxID,
            CarBoxNow = source.CarBoxNow,
            CarBoxMax = source.CarBoxMax,
            TerminalId = source.terminalId,
            VideoRtsp = source.VideoRtsp
        };
    }

    public static LaneMessageDto ToLaneMessageDto(this MsgInfo source)
    {
        return new LaneMessageDto
        {
            LaneNo = source.LaneNo,
            MsgType = source.MsgType,
            OccDateTime = source.OccDateTime,
            CollNo = source.CollNo,
            CarKind = source.CarKind,
            CarType = source.CarType,
            PayType = source.PayType,
            Cash = source.Cash,
            Receipt = source.Receipt,
            Exception = source.Exception,
            Peccancy = source.Peccancy,
            DevStatus = source.DevStatus,
            PromptMsg = source.PromptMsg,
            PlazaId = source.PlazaId
        };
    }

    public static OverloadWarningDto ToOverloadWarningDto(this OverloadWarning source)
    {
        return new OverloadWarningDto
        {
            Title = source.Title,
            Context = source.Context,
            Id = source.Id
        };
    }

    public static LaneSpecialDto ToLaneSpecialDto(this Lanespecial source)
    {
        return new LaneSpecialDto
        {
            Title = source.Title,
            Context = source.Context,
            LaneId = source.LaneId,
            SpecialCode = source.SpecialCode
        };
    }

    public static BulkTransportDto ToBulkTransportDto(this BulklyDto source)
    {
        return new BulkTransportDto
        {
            Head = source.Head?.ToMessageHeadDto(),
            SubHead = source.SubHead?.ToMessageSubHeadDto(),
            VehId = source.VehId,
            VehColor = source.VehColor,
            Alex = source.Alex,
            Weight = source.Weight,
            LargeWoods = source.LARGEWOODS?.ToLargeWoodsDto(),
            IsValid = source.IsValid,
            Title = source.Title
        };
    }

    public static BillInfoRequestDto ToBillInfoRequestDto(this BillInfoDto source)
    {
        return new BillInfoRequestDto
        {
            Head = source.Head?.ToMessageHeadDto(),
            SubHead = source.SubHead?.ToMessageSubHeadDto(),
            BillCode = source.billCode,
            BillNumber = source.billNumber
        };
    }

    public static ConfirmEnInfoDto ToConfirmEnInfoDto(this ConfirmEnInfo source)
    {
        return new ConfirmEnInfoDto
        {
            LaneId = source.laneId,
            PlazaId = source.plazaId,
            LaneNo = source.laneNo,
            GenTime = source.genTime,
            VehicleId = source.vehicleId,
            VehicleType = source.vehicleType,
            ResCount = source.resCount,
            RetQuery = source.retQuery,
            Code = source.code,
            Msg = source.msg,
            EnStations = source.enStations?.Select(ToEnStationDto).ToList() ?? new List<EnStationDto>()
        };
    }

    public static MessageHeadDto ToMessageHeadDto(this Head source)
    {
        return new MessageHeadDto
        {
            NetNo = source.NetNo,
            PlazaNo = source.PlazaNo,
            LaneId = source.LaneID,
            Ddhm = source.DDHM,
            LaneType = source.LaneType,
            MsgLen = source.MsgLen,
            MsgType = source.MsgType,
            MsgVersion = source.MsgVersion,
            Reserved = source.Reserved
        };
    }

    public static MessageSubHeadDto ToMessageSubHeadDto(this SubHead source)
    {
        return new MessageSubHeadDto
        {
            LaneMode = source.LaneMode,
            CETC = source.CETC,
            StaffId = source.StaffID,
            StaffName = source.StaffName,
            JobNo = source.JobNo,
            SquadId = source.SquadID,
            ShiftNo = source.ShiftNo
        };
    }

    public static LargeWoodsDto ToLargeWoodsDto(this LARGEWOODS source)
    {
        return new LargeWoodsDto
        {
            Lincense = source.LINCENSE,
            Plate = source.PLATE,
            EnStationId = source.EN_STATION_ID,
            ExStationId = source.EX_STATION_ID,
            StartPassDate = source.START_PASS_DATE,
            EndPassDate = source.END_PASS_DATE,
            CarsTotalWeight = source.CARS_TOTAL_WEIGHT,
            CarLength = source.CAR_LENGTH,
            CarWidth = source.CAR_WIDTH,
            CarHeight = source.CAR_HEIGHT,
            CarAxleNum = source.CAR_AXLENUM,
            Version = source.VERSION
        };
    }

    public static EnStationDto ToEnStationDto(this EnStations source)
    {
        return new EnStationDto
        {
            CardId = source.cardId,
            EnStationId = source.enStationId,
            EnTime = source.enTime,
            EnDateTime = source.enDateTime,
            EnTollLaneId = source.enTollLaneId,
            MediaNo = source.mediaNo,
            MediaType = source.mediaType,
            ResultVoucher = source.resultVoucher
        };
    }

    public static TcoWeightMessageDto ToTcoWeightMessageDto(this MsgWeightTCOCALL source)
    {
        return new TcoWeightMessageDto
        {
            Head = source.Head?.ToMessageHeadDto(),
            SubHead = source.SubHead?.ToMessageSubHeadDto(),
            WatcherId = (WatcherType)source.WatcherID,
            DialogType = (TcoDialogType)source.DlgType,
            Tran = source.MsgTcoTran?.ToTcoTranDto(),
            WeightFunctions = source.WeightFunctions,
            FareFormula = source.FareFormula,
            TimeOut = source.TimeOut
        };
    }

    public static TcoConfirmRequestDto ToTcoConfirmRequestDto(this TCOCall source)
    {
        return new TcoConfirmRequestDto
        {
            Head = source.Head?.ToMessageHeadDto(),
            SubHead = source.SubHead?.ToMessageSubHeadDto(),
            WatcherId = (WatcherType)source.WatcherID,
            DialogType = (TcoDialogType)source.DlgType,
            Tran = source.MsgTcoTran?.ToTcoTranDto(),
            TimeOut = source.TimeOut
        };
    }

    public static TcoTranDto ToTcoTranDto(this MsgTcoTran source)
    {
        return new TcoTranDto
        {
            TransNo = source.TransNO,
            WeightType = source.WeightType,
            CarClass = source.CarClass,
            ExitVehiKind = source.ExitVehiKind,
            Distance = source.Distance,
            ExitPlate = source.ExitPlate,
            InputPlate = source.InputPlate,
            DetectAxleCount = source.DetectAxleCount,
            DetectWeightTotal = source.DetectWeightTotal,
            Speed = source.Speed,
            OverloadWeight = source.OverloadWeight,
            OverloadReason = source.OverloadReason,
            FWeightTotal = source.FWeightTotal,
            Detail = source.Detail,
            WeightLimit = source.WeightLimit,
            EntryNetNo = source.EntryNetNo,
            EntryPlazaNo = source.EntryPlazaNo,
            EntryCarType = source.EntryCarType,
            VehicleType = source.VehicleType,
            VehicleTypeChinese = source.VehicleTypeChinese,
            EntryVehiTKind = source.EntryVehiTKind,
            EntryPlate = source.EntryPlate,
            EntryDhm = source.EntryDHM,
            DifPlaza = source.DifPlaza,
            DifPlate = source.DifPlate,
            DifClass = source.DifClass,
            DifType = source.DifType,
            UCar = source.UCar,
            TimeoutCar = source.TimeoutCar,
            CardNo = source.CardNo,
            EntryLaneId = source.EntryLaneID,
            EntryStationId = source.EntryStationID,
            EntryStationName = source.EntryStationName
        };
    }
}