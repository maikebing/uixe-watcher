namespace Uixe.Copilot.Contracts.Dtos;

public sealed class TrafficEventHistoryResponseDto
{
    public int Total { get; set; }

    public int PageNo { get; set; }

    public int PageSize { get; set; }

    public List<TrafficEventListItemDto> Items { get; set; } = new();
}