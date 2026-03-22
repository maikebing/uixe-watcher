using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Contracts.Extensions;

public static class LegacyLaneDtoMappings
{
	public static LaneStatusDto ToLaneStatusDto(this LegacyLaneStatusRequestDto source)
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
			TerminalId = source.TerminalId,
			VideoRtsp = source.VideoRtsp
		};
	}

	public static LaneMessageDto ToLaneMessageDto(this LegacyLaneMessageRequestDto source)
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

	public static OverloadWarningDto ToOverloadWarningDto(this LegacyOverloadWarningRequestDto source)
	{
		return new OverloadWarningDto
		{
			Title = source.Title,
			Context = source.Context,
			Id = source.Id
		};
	}

	public static LaneSpecialDto ToLaneSpecialDto(this LegacyLaneSpecialRequestDto source)
	{
		return new LaneSpecialDto
		{
			Title = source.Title,
			Context = source.Context,
			LaneId = source.LaneId,
			SpecialCode = source.SpecialCode
		};
	}
}