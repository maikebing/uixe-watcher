using Microsoft.Extensions.DependencyInjection;
using Uixe.Copilot.Application;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Infrastructure.TrafficEvents;

namespace Uixe.Copilot.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUixeCopilotInfrastructure(this IServiceCollection services)
    {
        services.AddUixeCopilotApplication();
        services.AddSingleton<ITrafficEventRepository, InMemoryTrafficEventRepository>();
        return services;
    }
}
