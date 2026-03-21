using Microsoft.Extensions.DependencyInjection;
using Uixe.Copilot.Application;

namespace Uixe.Copilot.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUixeCopilotInfrastructure(this IServiceCollection services)
    {
        services.AddUixeCopilotApplication();
        return services;
    }
}
