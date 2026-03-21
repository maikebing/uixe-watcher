using Microsoft.AspNetCore.Mvc.Testing;
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
}
