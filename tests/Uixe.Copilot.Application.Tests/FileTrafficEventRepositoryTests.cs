using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Infrastructure.Persistence;
using Uixe.Copilot.Infrastructure.Persistence.TrafficEvents;
using Xunit;

namespace Uixe.Copilot.Application.Tests;

public sealed class FileTrafficEventRepositoryTests
{
    [Fact]
    public async Task SaveAsync_AndGetByIdAsync_ShouldPersistToFile()
    {
        var tempFile = Path.Combine(Path.GetTempPath(), $"traffic-events-{Guid.NewGuid():N}.json");
        var repository = new FileTrafficEventRepository(new InfrastructureOptions
        {
            TrafficEventStoragePath = tempFile,
            TrafficEventRepositoryMode = "File"
        });

        await repository.SaveAsync(new TrafficEventPushRequestDto
        {
            RecordId = "file-evt-001",
            EventType = "文件持久化测试",
            LaneNo = "001"
        });

        var eventItem = await repository.GetByIdAsync("file-evt-001");

        Assert.NotNull(eventItem);
        Assert.Equal("001", eventItem!.LaneNo);

        if (File.Exists(tempFile))
        {
            File.Delete(tempFile);
        }
    }
}