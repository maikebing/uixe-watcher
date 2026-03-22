using Microsoft.Extensions.DependencyInjection;

namespace Uixe.Copilot.Agent;

public static class DependencyInjection
{
    public static IServiceCollection AddUixeCopilotAgentPlatform(this IServiceCollection services)
    {
        if (OperatingSystem.IsWindows())
        {
            services.AddWindowsAgentPlatform();
            return services;
        }

        if (OperatingSystem.IsLinux())
        {
            services.AddLinuxAgentPlatform();
            return services;
        }

        throw new PlatformNotSupportedException("Uixe.Copilot.Agent currently supports only Windows and Linux.");
    }
}
