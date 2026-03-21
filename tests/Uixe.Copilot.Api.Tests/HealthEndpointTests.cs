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
}
