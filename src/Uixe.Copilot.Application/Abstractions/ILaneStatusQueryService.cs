using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Application.Abstractions;

public interface ILaneStatusQueryService
{
    Task<IReadOnlyCollection<PlazaLaneSnapshotDto>> GetPlazaLaneSnapshotsAsync(CancellationToken cancellationToken = default);
}