namespace Uixe.Copilot.Contracts.Dtos;

public sealed class ConfirmEnInfoDto
{
    public string? LaneId { get; set; }

    public string? PlazaId { get; set; }

    public string? LaneNo { get; set; }

    public string? GenTime { get; set; }

    public string? VehicleId { get; set; }

    public int VehicleType { get; set; }

    public int ResCount { get; set; }

    public int RetQuery { get; set; }

    public int Code { get; set; }

    public string? Msg { get; set; }

    public List<EnStationDto> EnStations { get; set; } = new();
}
