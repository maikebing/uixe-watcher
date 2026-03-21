namespace Uixe.Copilot.Agent.Core.Models;

public sealed record LocalNotificationRequest(string Title, string Message, int TimeoutMilliseconds = 5000);