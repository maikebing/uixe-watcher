namespace Uixe.Copilot.Contracts.Dtos;

public sealed class LaneStatusDto
{
    public string? LaneNo { get; set; }

    public string? CollName { get; set; }

    public string? CollNo { get; set; }

    public string? ClientMsg { get; set; }

    public string? CarType { get; set; }

    public string? Money { get; set; }

    public string? CarKind { get; set; }

    public string? WrokMode { get; set; }

    public DateTime JobBeginTime { get; set; }

    public bool YuPengDengStatus { get; set; }

    public bool JiaoTongDengStatus { get; set; }

    public bool LanGanStatus { get; set; }

    public bool Coil1Status { get; set; }

    public bool Coil2Status { get; set; }

    public bool Coil3Status { get; set; }

    public bool Coil4Status { get; set; }

    public bool PrinterStatus { get; set; }

    public bool NetworkStatus { get; set; }

    public bool RSUStatus { get; set; }

    public bool ReaderStatus { get; set; }

    public bool WeightStatus { get; set; }

    public bool VPRStatus { get; set; }

    public bool CameraStatus { get; set; }

    public bool YellowStatus { get; set; }

    public bool QRPayStatus { get; set; }

    public bool BaoJingStatus { get; set; }

    public bool LWDStatus { get; set; }

    public int CarBoxID { get; set; }

    public int CarBoxNow { get; set; }

    public int CarBoxMax { get; set; }

    public string? TerminalId { get; set; }

    public string? VideoRtsp { get; set; }
}