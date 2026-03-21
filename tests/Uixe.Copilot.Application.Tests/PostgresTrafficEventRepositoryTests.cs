using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Infrastructure.Persistence;
using Uixe.Copilot.Infrastructure.Persistence.TrafficEvents;
using Xunit;

namespace Uixe.Copilot.Application.Tests;

public sealed class PostgresTrafficEventRepositoryTests
{
    [Fact]
    public async Task SaveAsync_AndGetByIdAsync_ShouldWork_WhenPostgresAvailable()
    {
        var connectionString = Environment.GetEnvironmentVariable("UIXE_TEST_POSTGRES_CONNECTION");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            return;
        }

        var repository = new PostgresTrafficEventRepository(new InfrastructureOptions
        {
            TrafficEventRepositoryMode = "Postgres",
            TrafficEventPostgresConnectionString = connectionString
        });

        await repository.SaveAsync(new TrafficEventPushRequestDto
        {
            RecordId = "pg-evt-001",
            EventType = "Postgres≤‚ ‘",
            LaneNo = "001"
        });

        var item = await repository.GetByIdAsync("pg-evt-001");
        Assert.NotNull(item);
    }
}