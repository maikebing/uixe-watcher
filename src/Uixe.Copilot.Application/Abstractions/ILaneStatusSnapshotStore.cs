using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Application.Abstractions;

public interface ILaneStatusSnapshotStore
{
    void Upsert(string plazaId, string laneNo, LaneStatusDto status, PlazaInfo? plaza, LaneInfo? lane);

    IReadOnlyCollection<PlazaLaneSnapshotDto> GetAll();
}