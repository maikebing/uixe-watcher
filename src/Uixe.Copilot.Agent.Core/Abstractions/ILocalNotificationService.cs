using Uixe.Copilot.Agent.Core.Models;

namespace Uixe.Copilot.Agent.Core.Abstractions;

public interface ILocalNotificationService
{
    Task ShowAsync(LocalNotificationRequest request, CancellationToken cancellationToken = default);
}