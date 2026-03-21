namespace Uixe.Copilot.Contracts.Dtos;

public sealed class EnStationDto
{
    public string? CardId { get; set; }

    public string? EnStationId { get; set; }

    public string? EnTime { get; set; }

    public DateTime EnDateTime { get; set; }

    public string? EnTollLaneId { get; set; }

    public string? MediaNo { get; set; }

    public int MediaType { get; set; }

    public int ResultVoucher { get; set; }
}
