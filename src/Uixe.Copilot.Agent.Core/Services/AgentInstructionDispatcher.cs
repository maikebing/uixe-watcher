using Microsoft.Extensions.Hosting;
using Uixe.Copilot.Agent.Core.Abstractions;
using Uixe.Copilot.Agent.Core.Models;

namespace Uixe.Copilot.Agent.Core.Services;

public sealed class AgentInstructionDispatcher(
    ILocalNotificationService notificationService,
    ISpeechService speechService,
    IVncLauncher vncLauncher,
    IWebViewService webViewService,
    IVideoPlayerService videoPlayerService,
    IHostApplicationLifetime applicationLifetime) : IAgentInstructionDispatcher
{
    public async Task<AgentInstructionResponse> DispatchAsync(AgentInstructionRequest request, CancellationToken cancellationToken = default)
    {
        var commandType = request.CommandType?.Trim().ToLowerInvariant();

        switch (commandType)
        {
            case "notify":
            case "notification":
                if (string.IsNullOrWhiteSpace(request.Title) || string.IsNullOrWhiteSpace(request.Message))
                {
                    return new AgentInstructionResponse(false, "Title and Message are required.");
                }

                await notificationService.ShowAsync(new LocalNotificationRequest(request.Title, request.Message), cancellationToken);

                if (request.PlaySpeech)
                {
                    await speechService.SpeakAsync(
                        new SpeechRequest(
                            request.Text ?? request.Message,
                            request.VoiceName,
                            request.Volume ?? 100,
                            request.Rate ?? 0),
                        cancellationToken);
                }

                break;

            case "speak":
            case "speech":
                if (string.IsNullOrWhiteSpace(request.Text))
                {
                    return new AgentInstructionResponse(false, "Text is required.");
                }

                await speechService.SpeakAsync(
                    new SpeechRequest(
                        request.Text,
                        request.VoiceName,
                        request.Volume ?? 100,
                        request.Rate ?? 0),
                    cancellationToken);
                break;

            case "vnc":
                if (string.IsNullOrWhiteSpace(request.Host))
                {
                    return new AgentInstructionResponse(false, "Host is required.");
                }

                await vncLauncher.LaunchAsync(
                    new VncLaunchRequest(request.Host, request.Port ?? 5900, request.Password, request.VncTitle),
                    cancellationToken);
                break;

            case "web":
            case "webview":
                if (string.IsNullOrWhiteSpace(request.Url))
                {
                    return new AgentInstructionResponse(false, "Url is required.");
                }

                await webViewService.OpenAsync(
                    new WebViewRequest(
                        request.Url,
                        request.WebTitle ?? "Uixe.Copilot",
                        request.Width ?? 1280,
                        request.Height ?? 800),
                    cancellationToken);
                break;

            case "video":
                var videoSource = request.VideoSource ?? request.Url;
                if (string.IsNullOrWhiteSpace(videoSource))
                {
                    return new AgentInstructionResponse(false, "VideoSource or Url is required.");
                }

                await videoPlayerService.PlayAsync(
                    new VideoPlaybackRequest(
                        videoSource,
                        request.VideoTitle ?? request.WebTitle ?? "Uixe.Copilot Video",
                        request.Width ?? 1280,
                        request.Height ?? 720,
                        request.AutoPlay,
                        request.KeepWindowOpen),
                    cancellationToken);
                break;

            default:
                return new AgentInstructionResponse(false, $"Unsupported command type: {request.CommandType}");
        }

        if (!request.KeepRunning)
        {
            applicationLifetime.StopApplication();
        }

        return new AgentInstructionResponse(true, "OK");
    }
}