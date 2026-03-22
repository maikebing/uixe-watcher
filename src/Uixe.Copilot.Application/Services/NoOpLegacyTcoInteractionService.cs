using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Contracts.Responses;

namespace Uixe.Copilot.Application.Services;

public sealed class NoOpLegacyTcoInteractionService : ILegacyTcoInteractionService
{
    public Task<ApiResult> ShowWeightMessageAsync(string plazaId, TcoWeightMessageDto message, CancellationToken cancellationToken = default)
        => Task.FromResult(new ApiResult(ApiCode.OK, $"Legacy weight TCO ignored for {plazaId}"));

    public Task<ApiResult> ShowTcoConfirmAsync(string plazaId, TcoConfirmRequestDto message, CancellationToken cancellationToken = default)
        => Task.FromResult(new ApiResult(ApiCode.OK, $"Legacy TCO confirm ignored for {plazaId}"));
}