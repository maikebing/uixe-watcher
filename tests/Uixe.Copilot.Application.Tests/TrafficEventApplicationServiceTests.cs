using Uixe.Copilot.Application.Services;
using Uixe.Copilot.Contracts.Dtos;
using Xunit;

namespace Uixe.Copilot.Application.Tests;

public sealed class TrafficEventApplicationServiceTests
{
    [Fact]
    public async Task SubmitAsync_ShouldReturnBadRequest_WhenLaneNoMissing()
    {
        var context = new InMemoryPlazaContextService();
        var workflow = new TrafficEventWorkflowService(context);
        var repository = new InMemoryTrafficEventRepository();
        var service = new TrafficEventApplicationService(workflow, context, new NoOpRealtimePushService(), repository);

        var response = await service.SubmitAsync(new TrafficEventPushRequestDto());

        Assert.Equal(1, response.Code);
    }

    [Fact]
    public async Task SubmitAsync_ShouldReturnSuccess_WhenLaneMatched()
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
                    Lanes = new List<LaneInfo> { new() { LaneNo = "001" } }
                }
            }
        });
        var workflow = new TrafficEventWorkflowService(context);
        var repository = new InMemoryTrafficEventRepository();
        var service = new TrafficEventApplicationService(workflow, context, new NoOpRealtimePushService(), repository);

        var response = await service.SubmitAsync(new TrafficEventPushRequestDto { LaneNo = "001" });

        Assert.Equal(0, response.Code);
    }

    [Fact]
    public async Task SubmitAsync_ShouldPersistEvent_WhenLaneMatched()
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
                    Lanes = new List<LaneInfo> { new() { LaneNo = "001" } }
                }
            }
        });

        var repository = new InMemoryTrafficEventRepository();
        var workflow = new TrafficEventWorkflowService(context);
        var service = new TrafficEventApplicationService(workflow, context, new NoOpRealtimePushService(), repository);

        await service.SubmitAsync(new TrafficEventPushRequestDto { RecordId = "evt-001", LaneNo = "001", EventType = "�ŶӸ澯" });

        var stored = await repository.GetByIdAsync("evt-001");
        Assert.NotNull(stored);
        Assert.Equal("001", stored!.LaneNo);
    }
}
