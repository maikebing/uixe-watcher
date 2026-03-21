namespace Uixe.Copilot.Contracts.Dtos;

public sealed class BulkTransportDto
{
    public MessageHeadDto? Head { get; set; }

    public MessageSubHeadDto? SubHead { get; set; }

    public string? VehId { get; set; }

    public int VehColor { get; set; }

    public int Alex { get; set; }

    public float Weight { get; set; }

    public LargeWoodsDto? LargeWoods { get; set; }

    public bool IsValid { get; set; }

    public string? Title { get; set; }
}
