using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Contracts.Responses;

namespace Uixe.Copilot.Api.Services;

public sealed class HttpLocalAgentCommandForwarder(
    HttpClient httpClient,
    IOptions<LocalAgentForwardingOptions> options) : ILocalAgentCommandForwarder
{
    public async Task<ApiResult<AgentCommandAckRequestDto>> ForwardAsync(AgentCommandRequestDto request, CancellationToken cancellationToken = default)
    {
        var payload = new
        {
            commandType = request.CommandType,
            title = request.Payload.Title,
            message = request.Payload.Message,
            text = request.Payload.Text,
            voiceName = request.Payload.VoiceName,
            volume = request.Payload.Volume,
            rate = request.Payload.Rate,
            playSpeech = request.Payload.PlaySpeech,
            host = request.Payload.Host,
            port = request.Payload.Port,
            password = request.Payload.Password,
            vncTitle = request.Payload.VncTitle,
            url = request.Payload.Url,
            webTitle = request.Payload.WebTitle,
            videoSource = request.Payload.VideoSource,
            videoTitle = request.Payload.VideoTitle,
            videoWindowKey = request.Payload.VideoWindowKey,
            width = request.Payload.Width,
            height = request.Payload.Height,
            keepRunning = request.Payload.KeepRunning
        };

        var response = await httpClient.PostAsJsonAsync(new Uri(new Uri(options.Value.BaseUrl), "/commands"), payload, cancellationToken);
        var result = await response.Content.ReadFromJsonAsync<LocalAgentCommandResponse>(cancellationToken: cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return new ApiResult<AgentCommandAckRequestDto>
            {
                code = (int)ApiCode.BadRequest,
                msg = result?.Message ?? "local agent forwarding failed",
                data = new AgentCommandAckRequestDto
                {
                    CommandId = request.CommandId,
                    AgentId = request.TargetAgentId ?? "local-agent",
                    Status = "failed",
                    Message = result?.Message ?? "local agent forwarding failed"
                }
            };
        }

        return new ApiResult<AgentCommandAckRequestDto>
        {
            code = (int)ApiCode.OK,
            msg = result?.Message ?? "OK",
            data = new AgentCommandAckRequestDto
            {
                CommandId = request.CommandId,
                AgentId = request.TargetAgentId ?? "local-agent",
                Status = "succeeded",
                Message = result?.Message ?? "OK"
            }
        };
    }

    private sealed class LocalAgentCommandResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}