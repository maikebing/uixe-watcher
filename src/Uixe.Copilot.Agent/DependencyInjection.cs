using Microsoft.Extensions.DependencyInjection;

namespace Uixe.Copilot.Agent;

public static class DependencyInjection
{
    public static IServiceCollection AddUixeCopilotAgentPlatform(this IServiceCollection services)
    {
#if WINDOWS
        services.AddWindowsAgentPlatform();
#else
        services.AddLinuxAgentPlatform();
#endif

        return services;
    }
}