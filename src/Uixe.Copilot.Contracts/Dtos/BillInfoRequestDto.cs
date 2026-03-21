namespace Uixe.Copilot.Contracts.Dtos;

public sealed class BillInfoRequestDto
{
    public MessageHeadDto? Head { get; set; }

    public MessageSubHeadDto? SubHead { get; set; }

    public string? BillCode { get; set; }

    public string? BillNumber { get; set; }
}
