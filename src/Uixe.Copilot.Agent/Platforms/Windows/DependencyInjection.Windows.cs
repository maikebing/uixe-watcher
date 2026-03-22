using Microsoft.Extensions.DependencyInjection;
using Uixe.Copilot.Agent.Core.Abstractions;
using Uixe.Copilot.Agent.Platforms.Windows.Services;

namespace Uixe.Copilot.Agent;

public static class WindowsDependencyInjection
{
    public static IServiceCollection AddWindowsAgentPlatform(this IServiceCollection services)
    {
        services.AddSingleton<IAgentTrayService, WindowsTrayService>();
        services.AddSingleton<ILocalNotificationService, WindowsNotificationService>();
        services.AddSingleton<ISpeechService, WindowsSpeechService>();
        services.AddSingleton<IVncLauncher, WindowsVncLauncher>();
        services.AddSingleton<IWebViewService, WindowsWebViewService>();
        services.AddSingleton<IVideoPlayerService, WindowsVideoPlayerService>();
        services.AddSingleton<IWindowsNativeHost, WindowsNativeHost>();
        return services;
    }
}