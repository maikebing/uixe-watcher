using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Infrastructure.Persistence;
using Uixe.Copilot.Infrastructure.Persistence.TrafficEvents;
using Xunit;

namespace Uixe.Copilot.Application.Tests;

public sealed class DatabaseTrafficEventRepositoryTests
{
    [Fact]
    public async Task SaveAsync_AndGetByIdAsync_ShouldPersistToSqlite()
    {
        var dbPath = Path.Combine(Path.GetTempPath(), $"traffic-events-{Guid.NewGuid():N}.db");
        var repository = new DatabaseTrafficEventRepository(new InfrastructureOptions
        {
            TrafficEventRepositoryMode = "Database",
            TrafficEventConnectionString = $"Data Source={dbPath}"
        });

        await repository.SaveAsync(new TrafficEventPushRequestDto
        {
            RecordId = "sqlite-evt-001",
            EventType = "SQLite≥÷æ√ªØ≤‚ ‘",
            LaneNo = "001",
            ImageList = "https://example.com/a.jpg,https://example.com/b.jpg"
        });

        var eventItem = await repository.GetByIdAsync("sqlite-evt-001");

        Assert.NotNull(eventItem);
        Assert.Equal("sqlite-evt-001", eventItem!.Id);
        Assert.Equal(2, eventItem.ImageUrls.Count);

        if (File.Exists(dbPath))
        {
            File.Delete(dbPath);
        }
    }
}