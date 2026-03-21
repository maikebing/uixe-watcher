namespace Uixe.Copilot.Contracts.Dtos;

public sealed class TrafficEventPlazaStatusDto
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Status { get; set; } = "online";

    public int LanesOnline { get; set; }

    public int LanesTotal { get; set; }

    public int Alerts { get; set; }
}
