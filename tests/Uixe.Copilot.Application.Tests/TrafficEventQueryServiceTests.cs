using Uixe.Copilot.Application.Services;
using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Infrastructure.TrafficEvents;
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
            LaneNo = "008",
            ImageList = "https://example.com/test.jpg,https://example.com/test-2.jpg",
            VideoList = "https://example.com/test.mp4,https://example.com/test-2.mp4"
        });

        var service = new TrafficEventQueryService(repository);
        var eventItem = await service.GetByIdAsync("evt-202");

        Assert.NotNull(eventItem);
        Assert.Equal("008", eventItem!.LaneNo);
        Assert.Equal("https://example.com/test.jpg", eventItem.ImageUrl);
        Assert.Equal("https://example.com/test.mp4", eventItem.VideoUrl);
        Assert.Equal(2, eventItem.ImageUrls.Count);
        Assert.Equal(2, eventItem.VideoUrls.Count);
    }

    [Fact]
    public async Task GetHistoryAsync_ShouldSupportPaging()
    {
        var repository = new InMemoryTrafficEventRepository();

        for (var i = 0; i < 3; i++)
        {
            await repository.SaveAsync(new TrafficEventPushRequestDto
            {
                RecordId = $"evt-page-{i}",
                EventType = "分页测试",
                LaneNo = $"00{i}"
            });
        }

        var service = new TrafficEventQueryService(repository);
        var result = await service.GetHistoryAsync(new TrafficEventHistoryQueryDto { PageNo = 2, PageSize = 1 });

        Assert.Equal(1, result.Items.Count);
        Assert.Equal(2, result.PageNo);
        Assert.Equal(1, result.PageSize);
    }
}