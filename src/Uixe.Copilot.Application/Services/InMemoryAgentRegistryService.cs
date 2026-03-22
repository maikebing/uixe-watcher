using System.Collections.Concurrent;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Contracts.Responses;

namespace Uixe.Copilot.Application.Services;

public sealed class InMemoryAgentRegistryService : IAgentRegistryService
{
    private readonly ConcurrentDictionary<string, AgentRecordDto> _agents = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, AgentCommandRequestDto> _commands = new(StringComparer.OrdinalIgnoreCase);
    private readonly ILocalAgentCommandForwarder _localAgentCommandForwarder;

    public InMemoryAgentRegistryService(ILocalAgentCommandForwarder localAgentCommandForwarder)
    {
        _localAgentCommandForwarder = localAgentCommandForwarder;
    }

    public Task<AgentRecordDto> RegisterAsync(AgentRegistrationRequestDto request, CancellationToken cancellationToken = default)
    {
        var record = new AgentRecordDto
        {
            AgentId = request.AgentId,
            MachineName = request.MachineName,
            Platform = request.Platform,
            Version = request.Version,
            ListenUrl = request.ListenUrl,
            BossName = request.BossName,
            PlazaId = request.PlazaId,
            PlazaName = request.PlazaName,
            AgentIp = request.AgentIp,
            Capabilities = request.Capabilities,
            Status = request.Status,
            LastSeenAt = DateTimeOffset.UtcNow
        };

        _agents[request.AgentId] = record;
        return Task.FromResult(record);
    }

    public Task<AgentRecordDto?> HeartbeatAsync(string agentId, AgentHeartbeatRequestDto request, CancellationToken cancellationToken = default)
    {
        if (!_agents.TryGetValue(agentId, out var record))
        {
            return Task.FromResult<AgentRecordDto?>(null);
        }

        record.Status = request.Status;
        record.LastSeenAt = request.LastSeenAt ?? DateTimeOffset.UtcNow;
        return Task.FromResult<AgentRecordDto?>(record);
    }

    public Task<ApiResult> UnregisterAsync(string agentId, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_agents.TryRemove(agentId, out _)
            ? new ApiResult(ApiCode.OK, $"Agent {agentId} removed")
            : new ApiResult(ApiCode.NotFound, $"Agent {agentId} not found"));
    }

    public Task<AgentConfigDto> GetConfigAsync(string agentId, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new AgentConfigDto());
    }

    public Task<ApiResult> AckConfigAsync(string agentId, AgentConfigAckRequestDto request, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new ApiResult(ApiCode.OK, $"Agent {agentId} config ack received"));
    }

    public Task<ApiResult<AgentCommandRequestDto>> SubmitCommandAsync(AgentCommandRequestDto request, CancellationToken cancellationToken = default)
    {
        return SubmitCommandInternalAsync(request, cancellationToken);
    }

    private async Task<ApiResult<AgentCommandRequestDto>> SubmitCommandInternalAsync(AgentCommandRequestDto request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.CommandId))
        {
            request.CommandId = Guid.NewGuid().ToString("N");
        }

        _commands[request.CommandId] = request;
        var forwardResult = await _localAgentCommandForwarder.ForwardAsync(request, cancellationToken);
        return new ApiResult<AgentCommandRequestDto>
        {
            code = forwardResult.code,
            msg = forwardResult.msg ?? "command accepted",
            data = request
        };
    }

    public Task<ApiResult<AgentCommandRequestDto>> GetCommandAsync(string commandId, CancellationToken cancellationToken = default)
    {
        if (_commands.TryGetValue(commandId, out var command))
        {
            return Task.FromResult(new ApiResult<AgentCommandRequestDto>
            {
                code = (int)ApiCode.OK,
                msg = "ok",
                data = command
            });
        }

        return Task.FromResult(new ApiResult<AgentCommandRequestDto>
        {
            code = (int)ApiCode.NotFound,
            msg = "command not found"
        });
    }

    public Task<ApiResult> AckCommandAsync(string commandId, AgentCommandAckRequestDto request, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_commands.ContainsKey(commandId)
            ? new ApiResult(ApiCode.OK, $"Command {commandId} ack: {request.Status}")
            : new ApiResult(ApiCode.NotFound, $"Command {commandId} not found"));
    }
}