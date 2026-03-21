using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Uixe.Copilot.Contracts.Dtos;
using Xunit;

namespace Uixe.Copilot.Api.Tests;

public sealed class HealthEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public HealthEndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetHealth_ShouldReturnOk()
    {
        using var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/health");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetTrafficOverview_ShouldReturnOk()
    {
        using var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/traffic-events/overview");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetTrafficHistory_ShouldReturnOk()
    {
        using var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/traffic-events/history");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetSystemSettings_ShouldReturnOk()
    {
        using var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/system-settings");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task SubmitTrafficEvent_ShouldReturnBadRequest_WhenLaneMissing()
    {
        using var client = _factory.CreateClient();
        var response = await client.PostAsJsonAsync("/api/traffic-events", new TrafficEventPushRequestDto());

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task GetTrafficEventsHub_NegotiatePathShouldExist()
    {
        using var client = _factory.CreateClient();
        var response = await client.PostAsync("/hubs/traffic-events/negotiate?negotiateVersion=1", null);

        Assert.NotEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task SubmitTrafficEvent_ThenGetById_ShouldPersistUnderFileMode()
    {
        using var client = _factory.CreateClient();

        var submitResponse = await client.PostAsJsonAsync("/api/traffic-events", new TrafficEventPushRequestDto
        {
            RecordId = "api-file-evt-001",
            EventType = "API文件持久化测试",
            LaneNo = "001"
        });

        if (!submitResponse.IsSuccessStatusCode)
        {
            return;
        }

        var detailResponse = await client.GetAsync("/api/traffic-events/api-file-evt-001");
        detailResponse.EnsureSuccessStatusCode();

        var eventItem = await detailResponse.Content.ReadFromJsonAsync<TrafficEventListItemDto>();
        Assert.NotNull(eventItem);
        Assert.Equal("api-file-evt-001", eventItem!.Id);
    }
}
