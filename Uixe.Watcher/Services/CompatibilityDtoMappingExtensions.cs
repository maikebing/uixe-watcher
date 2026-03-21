using System.Linq;
using Uixe.Copilot.Contracts.Dtos;
using Uixe.Watcher.Dtos;

namespace Uixe.Watcher.Services;

public static class CompatibilityDtoMappingExtensions
{
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
}
