namespace Uixe.Copilot.Contracts.Dtos;

public sealed class BossInfo
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? AgentIp { get; set; }

    public int PlazaType { get; set; }

    public List<PlazaInfo> Plazas { get; set; } = new();
}
