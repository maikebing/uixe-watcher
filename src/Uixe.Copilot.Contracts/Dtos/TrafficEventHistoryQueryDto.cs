namespace Uixe.Copilot.Contracts.Dtos;

public sealed class TrafficEventHistoryQueryDto
{
    public string? PlazaName { get; set; }

    public string? EventType { get; set; }

    public string? Status { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }
}