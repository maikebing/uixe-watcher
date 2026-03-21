using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Uixe.Copilot.Application;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Infrastructure.Persistence;
using Uixe.Copilot.Infrastructure.Persistence.TrafficEvents;
using Uixe.Copilot.Infrastructure.TrafficEvents;

namespace Uixe.Copilot.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUixeCopilotInfrastructure(this IServiceCollection services)
    {
        services.AddUixeCopilotApplication();
        services.AddOptions<InfrastructureOptions>();
        services.AddSingleton<InMemoryTrafficEventRepository>();
        services.AddSingleton<DatabaseTrafficEventRepository>();
        services.AddSingleton<PostgresTrafficEventRepository>(serviceProvider =>
        {
            var options = serviceProvider.GetService<IOptions<InfrastructureOptions>>()?.Value ?? new InfrastructureOptions();
            return new PostgresTrafficEventRepository(options);
        });
        services.AddSingleton<FileTrafficEventRepository>(serviceProvider =>
        {
            var options = serviceProvider.GetService<IOptions<InfrastructureOptions>>()?.Value ?? new InfrastructureOptions();
            return new FileTrafficEventRepository(options);
        });
        services.AddSingleton<ITrafficEventRepository>(serviceProvider =>
        {
            var options = serviceProvider.GetService<IOptions<InfrastructureOptions>>()?.Value ?? new InfrastructureOptions();
            if (string.Equals(options.TrafficEventRepositoryMode, "Database", StringComparison.OrdinalIgnoreCase))
            {
                return serviceProvider.GetRequiredService<DatabaseTrafficEventRepository>();
            }

            if (string.Equals(options.TrafficEventRepositoryMode, "Postgres", StringComparison.OrdinalIgnoreCase))
            {
                return serviceProvider.GetRequiredService<PostgresTrafficEventRepository>();
            }

            if (string.Equals(options.TrafficEventRepositoryMode, "File", StringComparison.OrdinalIgnoreCase))
            {
                return serviceProvider.GetRequiredService<FileTrafficEventRepository>();
            }

            return serviceProvider.GetRequiredService<InMemoryTrafficEventRepository>();
        });
        return services;
    }
}
