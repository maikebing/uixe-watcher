using Microsoft.Extensions.Hosting;
using Uixe.Copilot.Agent.Core.Abstractions;
using Uixe.Copilot.Agent.Core.Models;

namespace Uixe.Copilot.Agent.Core.Services;

public sealed class AgentCommandHostedService(
    AgentCommand command,
    ILocalNotificationService notificationService,
    ISpeechService speechService,
    IVncLauncher vncLauncher,
    IWebViewService webViewService,
    IHostApplicationLifetime applicationLifetime) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        if (!command.HasOperations)
        {
            return Task.CompletedTask;
        }

        _ = ExecuteAsync(cancellationToken);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        if (command.Notification is not null)
        {
            await notificationService.ShowAsync(command.Notification, cancellationToken);
        }

        if (command.Speech is not null)
        {
            await speechService.SpeakAsync(command.Speech, cancellationToken);
        }

        if (command.Vnc is not null)
        {
            await vncLauncher.LaunchAsync(command.Vnc, cancellationToken);
        }

        if (command.WebView is not null)
        {
            await webViewService.OpenAsync(command.WebView, cancellationToken);
        }

        if (!command.KeepRunning)
        {
            applicationLifetime.StopApplication();
        }
    }
}