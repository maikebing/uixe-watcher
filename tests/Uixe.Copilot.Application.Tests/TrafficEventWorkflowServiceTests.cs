using Xunit;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Application.Services;
using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Application.Tests;

public sealed class TrafficEventWorkflowServiceTests
{
    [Fact]
    public async Task EnqueueAsync_ShouldMatchLane_ByLaneNoSuffix()
    {
        var context = new InMemoryPlazaContextService();
        var service = new TrafficEventWorkflowService(context);
        var request = new TrafficEventPushRequestDto { LaneNo = "6501234" };
        var plazas = new[]
        {
            new PlazaInfo
            {
                Id = "P1",
                StationName = "Test",
                Lanes = new List<LaneInfo> { new() { LaneNo = "1234" } }
            }
        };

        var result = await service.EnqueueAsync(request, plazas);

        Assert.True(result);
    }

    [Fact]
    public async Task EnqueueAsync_ShouldUseContextPlazas_WhenArgumentEmpty()
    {
        var context = new InMemoryPlazaContextService();
        context.SetCurrentBoss(new BossInfo
        {
            Id = "boss-1",
            Name = "Boss",
            Plazas = new List<PlazaInfo>
            {
                new()
                {
                    Id = "P1",
                    StationName = "Station",
                    Lanes = new List<LaneInfo> { new() { LaneNo = "A-01" } }
                }
            }
        });
        var service = new TrafficEventWorkflowService(context);

        var result = await service.EnqueueAsync(new TrafficEventPushRequestDto { LaneNo = "A01" }, Array.Empty<PlazaInfo>());

        Assert.True(result);
    }
}
