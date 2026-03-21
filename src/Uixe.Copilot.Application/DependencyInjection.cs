using Microsoft.Extensions.DependencyInjection;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Application.Services;

namespace Uixe.Copilot.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddUixeCopilotApplication(this IServiceCollection services)
    {
        services.AddScoped<ITrafficEventWorkflowService, TrafficEventWorkflowService>();
        return services;
    }
}
