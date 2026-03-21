using Microsoft.Extensions.DependencyInjection;
using Uixe.Copilot.Agent.Core.Abstractions;
using Uixe.Copilot.Agent.Platforms.Linux.Services;

namespace Uixe.Copilot.Agent;

public static class LinuxDependencyInjection
{
    public static IServiceCollection AddLinuxAgentPlatform(this IServiceCollection services)
    {
        services.AddSingleton<IAgentTrayService, LinuxTrayService>();
        services.AddSingleton<ILocalNotificationService, LinuxNotificationService>();
        services.AddSingleton<ISpeechService, LinuxSpeechService>();
        services.AddSingleton<IVncLauncher, LinuxVncLauncher>();
        services.AddSingleton<IWebViewService, LinuxWebViewService>();
        return services;
    }
}