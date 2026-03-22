using Microsoft.Extensions.DependencyInjection;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Application.Services;

namespace Uixe.Copilot.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddUixeCopilotApplication(this IServiceCollection services)
    {
        services.AddSingleton<IPlazaContextService, InMemoryPlazaContextService>();
        services.AddSingleton<ISystemSettingsService, InMemorySystemSettingsService>();
        services.AddScoped<ILaneApplicationService, LaneApplicationService>();
        services.AddScoped<INotificationApplicationService, NotificationApplicationService>();
        services.AddScoped<ITcoWindowApplicationService, TcoWindowApplicationService>();
        services.AddScoped<ITrafficEventApplicationService, TrafficEventApplicationService>();
        services.AddScoped<ITrafficEventQueryService, TrafficEventQueryService>();
        services.AddScoped<ITrafficEventWorkflowService, TrafficEventWorkflowService>();
        services.AddScoped<IRealtimePushService, NoOpRealtimePushService>();
        return services;
    }
}
