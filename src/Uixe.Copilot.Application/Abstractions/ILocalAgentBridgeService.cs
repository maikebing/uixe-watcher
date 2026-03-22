using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Contracts.Responses;

namespace Uixe.Copilot.Application.Abstractions;

public interface ILocalAgentBridgeService
{
    Task<ApiResult> ShowNotificationAsync(
        string title,
        string message,
        bool playSpeech = false,
        string? speechText = null,
        CancellationToken cancellationToken = default);

    Task<ApiResult> SpeakAsync(
        string text,
        string? voiceName = null,
        CancellationToken cancellationToken = default);

    Task<ApiResult> OpenVncAsync(
        string host,
        int port = 5900,
        string? password = null,
        string? title = null,
        CancellationToken cancellationToken = default);

    Task<ApiResult> OpenWebAsync(
        string url,
        string? title = null,
        CancellationToken cancellationToken = default);

    Task<ApiResult> ShowTrafficEventAsync(
        PlazaInfo plaza,
        LaneInfo lane,
        TrafficEventPushRequestDto request,
        CancellationToken cancellationToken = default);
}
