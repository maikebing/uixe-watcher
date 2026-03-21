using Uixe.Copilot.Contracts.Responses;

namespace Uixe.Copilot.Application.Abstractions;

public interface ITcoWindowApplicationService
{
    Task<ApiResult> ShowWeightMessageAsync(string plazaId, object message, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowTcoConfirmAsync(string plazaId, object message, CancellationToken cancellationToken = default);
}
