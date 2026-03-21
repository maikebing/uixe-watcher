namespace Uixe.Copilot.Infrastructure.Persistence;

public sealed class InfrastructureOptions
{
    public const string SectionName = "Infrastructure";

    public string TrafficEventRepositoryMode { get; set; } = "InMemory";

    public string TrafficEventStoragePath { get; set; } = "App_Data/traffic-events.json";

    public string TrafficEventConnectionString { get; set; } = "Data Source=App_Data/traffic-events.db";

    public string TrafficEventPostgresConnectionString { get; set; } = "Host=localhost;Port=5432;Database=uixe_copilot;Username=postgres;Password=postgres";
}