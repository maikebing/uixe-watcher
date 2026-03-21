namespace Uixe.Copilot.Contracts.Dtos;

public sealed class TrafficEventHistoryResponseDto
{
    public int Total { get; set; }

    public List<TrafficEventListItemDto> Items { get; set; } = new();
}