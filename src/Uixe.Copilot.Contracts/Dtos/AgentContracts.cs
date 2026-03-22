namespace Uixe.Copilot.Contracts.Dtos;

public sealed class AgentRegistrationRequestDto
{
    public string AgentId { get; set; } = string.Empty;

    public string MachineName { get; set; } = string.Empty;

    public string Platform { get; set; } = "windows";

    public string Version { get; set; } = "v1";

    public string ListenUrl { get; set; } = string.Empty;

    public string? BossName { get; set; }

    public string? PlazaId { get; set; }

    public string? PlazaName { get; set; }

    public string? AgentIp { get; set; }

    public List<string> Capabilities { get; set; } = new();

    public string Status { get; set; } = "online";
}

public sealed class AgentHeartbeatRequestDto
{
    public string Status { get; set; } = "online";

    public DateTimeOffset? LastSeenAt { get; set; }
}

public sealed class AgentConfigDto
{
    public bool EnableNotification { get; set; } = true;

    public bool EnableSpeech { get; set; } = true;

    public bool EnableVnc { get; set; } = true;

    public bool EnableWeb { get; set; } = true;

    public bool EnableVideo { get; set; } = true;

    public string PreferredVoiceName { get; set; } = string.Empty;

    public int DefaultTimeoutSeconds { get; set; } = 30;

    public int DefaultRetryCount { get; set; } = 0;
}

public sealed class AgentConfigAckRequestDto
{
    public string AgentId { get; set; } = string.Empty;

    public DateTimeOffset AckAt { get; set; } = DateTimeOffset.UtcNow;

    public string Message { get; set; } = "config received";
}

public sealed class AgentCommandRequestDto
{
    public string CommandId { get; set; } = string.Empty;

    public string CommandType { get; set; } = string.Empty;

    public string? TargetAgentId { get; set; }

    public string? TargetPlazaId { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public int TimeoutSeconds { get; set; } = 30;

    public string? IdempotencyKey { get; set; }

    public string SourceSystem { get; set; } = "Uixe.Copilot.Api";

    public AgentCommandPayloadDto Payload { get; set; } = new();
}

public sealed class AgentCommandPayloadDto
{
    public string? Title { get; set; }

    public string? Message { get; set; }

    public string? Text { get; set; }

    public string? VoiceName { get; set; }

    public decimal? Volume { get; set; }

    public decimal? Rate { get; set; }

    public string? Host { get; set; }

    public int? Port { get; set; }

    public string? Password { get; set; }

    public string? VncTitle { get; set; }

    public string? Url { get; set; }

    public string? WebTitle { get; set; }

    public string? VideoSource { get; set; }

    public string? VideoTitle { get; set; }

    public string? VideoWindowKey { get; set; }

    public int? Width { get; set; }

    public int? Height { get; set; }

    public bool PlaySpeech { get; set; }

    public bool KeepRunning { get; set; } = true;
}

public sealed class AgentCommandAckRequestDto
{
    public string CommandId { get; set; } = string.Empty;

    public string AgentId { get; set; } = string.Empty;

    public string Status { get; set; } = "accepted";

    public string? Message { get; set; }

    public DateTimeOffset? StartedAt { get; set; }

    public DateTimeOffset? FinishedAt { get; set; }

    public string? ErrorCode { get; set; }

    public string? ErrorDetail { get; set; }
}

public sealed class AgentRecordDto
{
    public string AgentId { get; set; } = string.Empty;

    public string MachineName { get; set; } = string.Empty;

    public string Platform { get; set; } = "windows";

    public string Version { get; set; } = "v1";

    public string ListenUrl { get; set; } = string.Empty;

    public string? BossName { get; set; }

    public string? PlazaId { get; set; }

    public string? PlazaName { get; set; }

    public string? AgentIp { get; set; }

    public List<string> Capabilities { get; set; } = new();

    public string Status { get; set; } = "online";

    public DateTimeOffset LastSeenAt { get; set; } = DateTimeOffset.UtcNow;
}