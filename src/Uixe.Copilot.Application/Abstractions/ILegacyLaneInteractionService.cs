using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Application.Abstractions;

public interface ILegacyLaneInteractionService
{
    Task<bool> ShowBulkTransportAsync(object plazaHost, string laneId, BulkTransportDto dto, CancellationToken cancellationToken = default);

    Task<bool> ShowBillInfoAsync(object plazaHost, string laneId, BillInfoRequestDto dto, CancellationToken cancellationToken = default);

    Task<bool> ShowConfirmEnInfoAsync(object plazaHost, ConfirmEnInfoDto dto, CancellationToken cancellationToken = default);
}
