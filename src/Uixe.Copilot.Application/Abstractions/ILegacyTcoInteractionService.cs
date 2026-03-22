using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Contracts.Responses;

namespace Uixe.Copilot.Application.Abstractions;

public interface ILegacyTcoInteractionService
{
    Task<ApiResult> ShowWeightMessageAsync(string plazaId, TcoWeightMessageDto message, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowTcoConfirmAsync(string plazaId, TcoConfirmRequestDto message, CancellationToken cancellationToken = default);
}
