using Uixe.Copilot.Contracts.Responses;
using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Application.Abstractions;

public sealed class TrafficEventTargetResolutionResult
{
    public object? DisplayHost { get; set; }

    public object? Plaza { get; set; }

    public object? Lane { get; set; }

    public object? FormRequest { get; set; }
}

public interface ILegacyPlazaUiBridge
{
    Task<ApiResult> ShowLaneStatusAsync(string plazaId, string laneNo, object status, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowMessageAsync(string plazaId, object message, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowOverloadAlarmAsync(string plazaId, string title, string context, bool playSpeech, CancellationToken cancellationToken = default);

    Task<ApiResult> ShowLaneSpecialAsync(string plazaId, object message, CancellationToken cancellationToken = default);

    void PlayAlertRing();

    object? GetTrafficEventDisplayHost(string plazaId);

    TrafficEventTargetResolutionResult? TryResolveTrafficEventTarget(TrafficEventPushRequestDto request);
}