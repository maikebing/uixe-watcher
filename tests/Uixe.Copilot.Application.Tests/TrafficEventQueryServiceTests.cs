using Uixe.Copilot.Application.Services;
using Uixe.Copilot.Contracts.Dtos;
using Xunit;

namespace Uixe.Copilot.Application.Tests;

public sealed class TrafficEventQueryServiceTests
{
    [Fact]
    public async Task GetOverviewAsync_ShouldReturnPersistedEvents()
    {
        var repository = new InMemoryTrafficEventRepository();
        await repository.SaveAsync(new TrafficEventPushRequestDto
        {
            RecordId = "evt-101",
            EventType = "排队告警",
            LaneNo = "103"
        });

        var service = new TrafficEventQueryService(repository);
        var overview = await service.GetOverviewAsync();

        Assert.Single(overview.Events);
        Assert.Equal("evt-101", overview.Events[0].Id);
        Assert.Equal(1, overview.TodayEvents);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnPersistedEvent()
    {
        var repository = new InMemoryTrafficEventRepository();
        await repository.SaveAsync(new TrafficEventPushRequestDto
        {
            RecordId = "evt-202",
            EventType = "入口确认",
            LaneNo = "008"
        });

        var service = new TrafficEventQueryService(repository);
        var eventItem = await service.GetByIdAsync("evt-202");

        Assert.NotNull(eventItem);
        Assert.Equal("008", eventItem!.LaneNo);
    }
}