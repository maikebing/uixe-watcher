using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Contracts.Responses;
using Xunit;

namespace Uixe.Copilot.Api.Tests;

public sealed class AgentsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public AgentsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<ILocalAgentCommandForwarder, FakeLocalAgentCommandForwarder>();
            });
        });
    }

    [Fact]
    public async Task Register_Heartbeat_And_Command_ShouldReturnOk()
    {
        using var client = _factory.CreateClient();

        var registerResponse = await client.PostAsJsonAsync("/api/agents/register", new AgentRegistrationRequestDto
        {
            AgentId = "agent-test-01",
            MachineName = "machine-01",
            ListenUrl = "http://127.0.0.1:17173/",
            Capabilities = new List<string> { "notification", "speech" }
        });
        registerResponse.EnsureSuccessStatusCode();

        var heartbeatResponse = await client.PutAsJsonAsync("/api/agents/agent-test-01/heartbeat", new AgentHeartbeatRequestDto
        {
            Status = "online"
        });
        heartbeatResponse.EnsureSuccessStatusCode();

        var commandResponse = await client.PostAsJsonAsync("/api/agents/commands", new AgentCommandRequestDto
        {
            CommandType = "notification",
            TargetAgentId = "agent-test-01",
            Payload = new AgentCommandPayloadDto
            {
                Title = "测试提醒",
                Message = "来自 API 的转发测试"
            }
        });
        commandResponse.EnsureSuccessStatusCode();

        var commandResult = await commandResponse.Content.ReadFromJsonAsync<ApiResult<AgentCommandRequestDto>>();
        Assert.NotNull(commandResult);
        Assert.Equal((int)ApiCode.OK, commandResult!.code);
        Assert.Equal("notification", commandResult.data!.CommandType);
    }

    [Fact]
    public async Task GetMissingCommand_ShouldReturnNotFound()
    {
        using var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/agents/commands/not-exists");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private sealed class FakeLocalAgentCommandForwarder : ILocalAgentCommandForwarder
    {
        public Task<ApiResult<AgentCommandAckRequestDto>> ForwardAsync(AgentCommandRequestDto request, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new ApiResult<AgentCommandAckRequestDto>
            {
                code = (int)ApiCode.OK,
                msg = "forwarded by fake agent",
                data = new AgentCommandAckRequestDto
                {
                    CommandId = request.CommandId,
                    AgentId = request.TargetAgentId ?? "fake-agent",
                    Status = "succeeded",
                    Message = "forwarded by fake agent"
                }
            });
        }
    }
}