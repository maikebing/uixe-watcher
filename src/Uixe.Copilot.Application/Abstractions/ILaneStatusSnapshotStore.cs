using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Application.Abstractions;

public interface ILaneStatusSnapshotStore
{
    void Upsert(string plazaId, string laneNo, LaneStatusDto status, PlazaInfo? plaza, LaneInfo? lane);

    void MarkLaneLost(string plazaId, string laneNo, PlazaInfo? plaza, LaneInfo? lane);

    void AddMessage(string plazaId, LaneMessageDto message, PlazaInfo? plaza, LaneInfo? lane);

    void AddOverloadAlert(string plazaId, OverloadWarningDto warning, PlazaInfo? plaza, LaneInfo? lane);

    void AddLaneSpecial(string plazaId, LaneSpecialDto message, PlazaInfo? plaza, LaneInfo? lane);

    IReadOnlyCollection<PlazaLaneSnapshotDto> GetAll();
}