using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Contracts.Responses;

namespace Uixe.Watcher.Services;

public sealed class LocalAgentBridgeService : ILocalAgentBridgeService
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly HttpClient _httpClient = new();
    private readonly AppSettings _settings;
    private readonly ILogger<LocalAgentBridgeService> _logger;

    public LocalAgentBridgeService(IOptions<AppSettings> options, ILogger<LocalAgentBridgeService> logger)
    {
        _settings = options.Value;
        _logger = logger;
    }

    public Task<ApiResult> ShowNotificationAsync(
        string title,
        string message,
        bool playSpeech = false,
        string? speechText = null,
        CancellationToken cancellationToken = default)
    {
        return SendCommandAsync(
            new AgentCommandRequest
            {
                CommandType = "notification",
                Title = title,
                Message = message,
                PlaySpeech = playSpeech,
                Text = speechText,
                VoiceName = _settings.AgentVoiceName
            },
            cancellationToken);
    }

    public Task<ApiResult> SpeakAsync(
        string text,
        string? voiceName = null,
        CancellationToken cancellationToken = default)
    {
        return SendCommandAsync(
            new AgentCommandRequest
            {
                CommandType = "speech",
                Text = text,
                VoiceName = voiceName ?? _settings.AgentVoiceName
            },
            cancellationToken);
    }

    public Task<ApiResult> OpenVncAsync(
        string host,
        int port = 5900,
        string? password = null,
        string? title = null,
        CancellationToken cancellationToken = default)
    {
        return SendCommandAsync(
            new AgentCommandRequest
            {
                CommandType = "vnc",
                Host = host,
                Port = port,
                Password = password,
                VncTitle = title
            },
            cancellationToken);
    }

    public Task<ApiResult> OpenWebAsync(
        string url,
        string? title = null,
        CancellationToken cancellationToken = default)
    {
        return SendCommandAsync(
            new AgentCommandRequest
            {
                CommandType = "web",
                Url = url,
                WebTitle = title
            },
            cancellationToken);
    }

    public async Task<ApiResult> ShowTrafficEventAsync(
        PlazaInfo plaza,
        LaneInfo lane,
        TrafficEventPushRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var title = $"{plaza.StationName} {lane.LaneNo} 交通事件";
        var message = BuildTrafficEventSummary(plaza, lane, request);
        var notificationResult = await ShowNotificationAsync(
            title,
            message,
            playSpeech: !_settings.laneVideoMute,
            speechText: message,
            cancellationToken: cancellationToken);

        if (notificationResult.Code != ApiCode.OK)
        {
            return notificationResult;
        }

        var route = string.IsNullOrWhiteSpace(request.RecordId)
            ? "/events"
            : $"/events/{Uri.EscapeDataString(request.RecordId)}";

        return await OpenWebAsync(BuildCopilotUrl(route), "Uixe.Copilot", cancellationToken);
    }

    private async Task<ApiResult> SendCommandAsync(AgentCommandRequest payload, CancellationToken cancellationToken)
    {
        try
        {
            var endpoint = $"{GetAgentBaseUrl().TrimEnd('/')}/commands";
            using var content = new StringContent(JsonSerializer.Serialize(payload, JsonOptions), Encoding.UTF8, "application/json");
            using var response = await _httpClient.PostAsync(endpoint, content, cancellationToken);
            var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
            var commandResponse = JsonSerializer.Deserialize<AgentCommandResponse>(responseBody, JsonOptions);

            if (response.IsSuccessStatusCode && commandResponse?.Success == true)
            {
                return new ApiResult(ApiCode.OK, commandResponse.Message);
            }

            var errorMessage = commandResponse?.Message
                ?? $"Agent command failed with status {(int)response.StatusCode}.";

            _logger.LogWarning("Agent command {CommandType} failed: {Message}", payload.CommandType, errorMessage);
            return new ApiResult(ApiCode.Fail, errorMessage);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unable to send local agent command {CommandType}", payload.CommandType);
            return new ApiResult(ApiCode.Fail, ex.Message);
        }
    }

    private string GetAgentBaseUrl()
    {
        return string.IsNullOrWhiteSpace(_settings.AgentBaseUrl)
            ? "http://127.0.0.1:17173"
            : _settings.AgentBaseUrl;
    }

    private string BuildCopilotUrl(string route)
    {
        var baseUrl = string.IsNullOrWhiteSpace(_settings.CopilotWebBaseUrl)
            ? "http://127.0.0.1:9999"
            : _settings.CopilotWebBaseUrl;

        var normalizedRoute = route.StartsWith("/", StringComparison.Ordinal) ? route : $"/{route}";
        var trimmedBase = baseUrl.TrimEnd('/');

        if (trimmedBase.Contains('#', StringComparison.Ordinal))
        {
            var parts = trimmedBase.Split('#', 2, StringSplitOptions.None);
            return $"{parts[0]}#${normalizedRoute}".Replace("#$", "#", StringComparison.Ordinal);
        }

        return $"{trimmedBase}/#{normalizedRoute}";
    }

    private static string BuildTrafficEventSummary(PlazaInfo plaza, LaneInfo lane, TrafficEventPushRequestDto request)
    {
        var eventType = string.IsNullOrWhiteSpace(request.EventType) ? "未知事件" : request.EventType;
        var queueLength = request.MaxQueueLen.HasValue ? $"，最大排队 {request.MaxQueueLen:0.##} 米" : string.Empty;
        return $"{plaza.StationName} {lane.LaneNo} 车道出现 {eventType}{queueLength}";
    }

    private sealed class AgentCommandRequest
    {
        public string CommandType { get; set; } = string.Empty;

        public string? Title { get; set; }

        public string? Message { get; set; }

        public string? Text { get; set; }

        public string? VoiceName { get; set; }

        public bool PlaySpeech { get; set; }

        public string? Host { get; set; }

        public int? Port { get; set; }

        public string? Password { get; set; }

        public string? VncTitle { get; set; }

        public string? Url { get; set; }

        public string? WebTitle { get; set; }

        public bool KeepRunning { get; set; } = true;
    }

    private sealed class AgentCommandResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}
