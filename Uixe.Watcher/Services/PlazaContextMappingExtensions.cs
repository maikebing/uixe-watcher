using System.Linq;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;
using Uixe.Watcher.Dtos;

namespace Uixe.Watcher.Services;

public static class PlazaContextMappingExtensions
{
    public static BossInfo ToBossInfo(this T_Boss source)
    {
        return new BossInfo
        {
            Id = source.Id ?? string.Empty,
            Name = source.Name ?? string.Empty,
            PlazaType = (int)source.PlazaType,
            Plazas = source.Plazas?.Select(ToPlazaInfo).ToList() ?? new List<PlazaInfo>()
        };
    }

    public static PlazaInfo ToPlazaInfo(this T_Plaza source)
    {
        return new PlazaInfo
        {
            Id = source.Id ?? string.Empty,
            StationId = source.StationId,
            StationName = source.StationName ?? string.Empty,
            Lanes = source.Lanes?.Select(ToLaneInfo).ToList() ?? new List<LaneInfo>()
        };
    }

    public static LaneInfo ToLaneInfo(this T_Lane source)
    {
        return new LaneInfo
        {
            Id = source.Id ?? string.Empty,
            LaneId = source.LaneId,
            LaneNo = source.LaneNo
        };
    }
}
