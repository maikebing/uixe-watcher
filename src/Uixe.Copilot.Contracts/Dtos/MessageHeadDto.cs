namespace Uixe.Copilot.Contracts.Dtos;

public sealed class MessageHeadDto
{
    public string? NetNo { get; set; }

    public string? PlazaNo { get; set; }

    public string? LaneId { get; set; }

    public string? Ddhm { get; set; }

    public int LaneType { get; set; }

    public string? MsgLen { get; set; }

    public string? MsgType { get; set; }

    public string? MsgVersion { get; set; }

    public string? Reserved { get; set; }
}
