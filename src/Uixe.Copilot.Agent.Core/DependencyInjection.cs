using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Uixe.Copilot.Agent.Core.Services;

namespace Uixe.Copilot.Agent.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddUixeCopilotAgentCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<Models.AgentOptions>(configuration.GetSection(Models.AgentOptions.SectionName));
        services.Configure<Models.AgentHttpOptions>(configuration.GetSection(Models.AgentHttpOptions.SectionName));
        services.AddSingleton<Abstractions.IAgentInstructionDispatcher, AgentInstructionDispatcher>();
        services.AddHostedService<AgentBootstrapHostedService>();
        services.AddHostedService<AgentCommandHostedService>();
        services.AddHostedService<AgentHttpCommandHostedService>();
        return services;
    }
}