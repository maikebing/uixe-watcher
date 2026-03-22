namespace Uixe.Copilot.Contracts.Dtos;

public sealed class LaneStatusSnapshotDto
{
    public string PlazaId { get; set; } = string.Empty;

    public string PlazaName { get; set; } = string.Empty;

    public string LaneId { get; set; } = string.Empty;

    public string LaneNo { get; set; } = string.Empty;

    public string Status { get; set; } = "offline";

    public bool HasWarning { get; set; }

    public string LastMessage { get; set; } = string.Empty;

    public string LastHeartbeat { get; set; } = string.Empty;

    public string? CollectorName { get; set; }

    public string? WorkMode { get; set; }

    public string? VideoRtsp { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool NetworkStatus { get; set; }

    public bool CameraStatus { get; set; }

    public bool PrinterStatus { get; set; }

    public bool BarrierStatus { get; set; }

    public bool IsLost { get; set; }

    public string? LastMessageType { get; set; }

    public DateTime? LastMessageTime { get; set; }

    public List<LaneMessageSnapshotDto> Messages { get; set; } = new();

    public List<LaneAlertSnapshotDto> Alerts { get; set; } = new();
}

public sealed class LaneMessageSnapshotDto
{
    public string Type { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string Time { get; set; } = string.Empty;
}

public sealed class LaneAlertSnapshotDto
{
    public string Category { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string Time { get; set; } = string.Empty;
}

public sealed class PlazaLaneSnapshotDto
{
    public string PlazaId { get; set; } = string.Empty;

    public string PlazaName { get; set; } = string.Empty;

    public int LanesOnline { get; set; }

    public int LanesTotal { get; set; }

    public int Alerts { get; set; }

    public List<LaneStatusSnapshotDto> Lanes { get; set; } = new();
}