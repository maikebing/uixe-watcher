using Uixe.Copilot.Contracts.Responses;

namespace Uixe.Copilot.Application.Abstractions;

public interface INotificationApplicationService
{
    Task<ApiResult> ShowWeightMessageAsync(string plazaId, object message, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowTcoConfirmAsync(string plazaId, object message, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowOverloadAlarmAsync(string plazaId, string title, string context, bool playSpeech, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowLaneSpecialAsync(string plazaId, object message, CancellationToken cancellationToken = default);
}
