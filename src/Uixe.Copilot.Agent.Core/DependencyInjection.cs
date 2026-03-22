using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Uixe.Copilot.Agent.Core.Models;
using Uixe.Copilot.Agent.Core.Services;

namespace Uixe.Copilot.Agent.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddUixeCopilotAgentCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();
        services.AddSingleton<IOptions<AgentOptions>>(_ => Options.Create(BuildAgentOptions(configuration)));
        services.AddSingleton<IOptions<AgentHttpOptions>>(_ => Options.Create(BuildAgentHttpOptions(configuration)));
        services.AddSingleton<Abstractions.IAgentInstructionDispatcher, AgentInstructionDispatcher>();
        services.AddSingleton<Abstractions.IAgentWhoAmIService, AgentWhoAmIService>();
        services.AddHostedService<AgentBootstrapHostedService>();
        services.AddHostedService<AgentCommandHostedService>();
        services.AddHostedService<AgentHttpCommandHostedService>();
        return services;
    }

    private static AgentOptions BuildAgentOptions(IConfiguration configuration)
    {
        var section = configuration.GetSection(AgentOptions.SectionName);
        var laneBossServer = section[nameof(AgentOptions.LaneBossServer)] ?? configuration[nameof(AgentOptions.LaneBossServer)];
        var forceLocalhostSetting = section[nameof(AgentOptions.ForceLocalhostInDebugBuild)] ?? configuration[nameof(AgentOptions.ForceLocalhostInDebugBuild)];

        return new AgentOptions
        {
            ApplicationName = section[nameof(AgentOptions.ApplicationName)] ?? "Uixe.Copilot.Agent",
            TrayTooltip = section[nameof(AgentOptions.TrayTooltip)] ?? "Uixe.Copilot.Agent",
            WebDashboardUrl = section[nameof(AgentOptions.WebDashboardUrl)] ?? "http://127.0.0.1:9999",
            LaneBossServer = laneBossServer ?? "http://127.0.0.1/",
            LinuxTrayCommand = section[nameof(AgentOptions.LinuxTrayCommand)] ?? "zenity",
            LinuxSpeechCommand = section[nameof(AgentOptions.LinuxSpeechCommand)] ?? "spd-say",
            LinuxNotificationCommand = section[nameof(AgentOptions.LinuxNotificationCommand)] ?? "notify-send",
            LinuxBrowserCommand = section[nameof(AgentOptions.LinuxBrowserCommand)] ?? "xdg-open",
            LinuxVncCommand = section[nameof(AgentOptions.LinuxVncCommand)] ?? "xdg-open",
            LinuxVideoPlayerCommand = section[nameof(AgentOptions.LinuxVideoPlayerCommand)] ?? "xdg-open",
            ForceLocalhostInDebugBuild = bool.TryParse(forceLocalhostSetting, out var forceLocalhostInDebugBuild)
                ? forceLocalhostInDebugBuild
                : true
        };
    }

    private static AgentHttpOptions BuildAgentHttpOptions(IConfiguration configuration)
    {
        var section = configuration.GetSection(AgentHttpOptions.SectionName);

        return new AgentHttpOptions
        {
            ListenUrl = section[nameof(AgentHttpOptions.ListenUrl)] ?? "http://127.0.0.1:17173/"
        };
    }
}
