using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Contracts.Responses;

namespace Uixe.Copilot.Application.Services;

public sealed class TcoWindowApplicationService : ITcoWindowApplicationService
{
    public Task<ApiResult> ShowWeightMessageAsync(string plazaId, TcoWeightMessageDto message, CancellationToken cancellationToken = default)
        => Task.FromResult(new ApiResult(ApiCode.OK, $"Weight TCO window queued for {plazaId}"));

    public Task<ApiResult> ShowTcoConfirmAsync(string plazaId, TcoConfirmRequestDto message, CancellationToken cancellationToken = default)
        => Task.FromResult(new ApiResult(ApiCode.OK, $"TCO confirm window queued for {plazaId}"));
}
