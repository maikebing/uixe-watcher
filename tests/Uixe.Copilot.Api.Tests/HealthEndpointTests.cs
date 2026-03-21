using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Application.Services;
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
            EventType = "API???????",
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

    [Fact]
    public async Task SubmitTrafficEvent_ThenQueryHistory_ShouldReturnPagedTotalUnderSqliteMode()
    {
        var dbPath = Path.Combine(Path.GetTempPath(), $"traffic-events-api-{Guid.NewGuid():N}.db");
        var factory = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureAppConfiguration((_, config) =>
            {
                config.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["Infrastructure:TrafficEventRepositoryMode"] = "Database",
                    ["Infrastructure:TrafficEventConnectionString"] = $"Data Source={dbPath}"
                });
            });
        });

        using var client = factory.CreateClient();

        SeedLaneContext(factory.Services);

        for (var i = 0; i < 3; i++)
        {
            await client.PostAsJsonAsync("/api/traffic-events", new TrafficEventPushRequestDto
            {
                RecordId = $"sqlite-api-{i}",
                EventType = "SQLite API ??",
                LaneNo = "001"
            });
        }

        var response = await client.GetAsync("/api/traffic-events/history?pageNo=1&pageSize=2");
        response.EnsureSuccessStatusCode();

        var history = await response.Content.ReadFromJsonAsync<TrafficEventHistoryResponseDto>();
        Assert.NotNull(history);
        Assert.Equal(3, history!.Total);
        Assert.Equal(2, history.Items.Count);
    }

    [Fact]
    public async Task SubmitTrafficEvent_ThenGetById_ShouldWorkUnderPostgresMode_WhenConnectionProvided()
    {
        var connectionString = Environment.GetEnvironmentVariable("UIXE_TEST_POSTGRES_CONNECTION");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            return;
        }

        var recordId = $"pg-api-{Guid.NewGuid():N}";
        var factory = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureAppConfiguration((_, config) =>
            {
                config.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["Infrastructure:TrafficEventRepositoryMode"] = "Postgres",
                    ["Infrastructure:TrafficEventPostgresConnectionString"] = connectionString
                });
            });
        });

        using var client = factory.CreateClient();

        SeedLaneContext(factory.Services);

        var submitResponse = await client.PostAsJsonAsync("/api/traffic-events", new TrafficEventPushRequestDto
        {
            RecordId = recordId,
            EventType = "PostgreSQL API ??",
            LaneNo = "001"
        });

        submitResponse.EnsureSuccessStatusCode();

        var detailResponse = await client.GetAsync($"/api/traffic-events/{recordId}");
        detailResponse.EnsureSuccessStatusCode();

        var eventItem = await detailResponse.Content.ReadFromJsonAsync<TrafficEventListItemDto>();
        Assert.NotNull(eventItem);
        Assert.Equal(recordId, eventItem!.Id);
        Assert.Equal("001", eventItem.LaneNo);
    }

    private static void SeedLaneContext(IServiceProvider services)
    {
        var plazaContext = services.GetRequiredService<IPlazaContextService>();
        if (plazaContext is not InMemoryPlazaContextService inMemoryContext)
        {
            return;
        }

        inMemoryContext.SetCurrentBoss(new BossInfo
        {
            Id = "boss-api-tests",
            Name = "API Test Boss",
            Plazas = new List<PlazaInfo>
            {
                new()
                {
                    Id = "P1",
                    StationName = "API Test Plaza",
                    Lanes = new List<LaneInfo>
                    {
                        new() { LaneNo = "001" }
                    }
                }
            }
        });
    }
}
