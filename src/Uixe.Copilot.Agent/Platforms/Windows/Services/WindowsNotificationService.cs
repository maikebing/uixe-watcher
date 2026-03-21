using Uixe.Copilot.Agent.Core.Abstractions;
using Uixe.Copilot.Agent.Core.Models;

namespace Uixe.Copilot.Agent.Platforms.Windows.Services;

public sealed class WindowsNotificationService(IWindowsNativeHost nativeHost) : ILocalNotificationService
{
    public Task ShowAsync(LocalNotificationRequest request, CancellationToken cancellationToken = default)
    {
        var title = request.Title.Replace("'", "''");
        var message = request.Message.Replace("'", "''");
        return nativeHost.LaunchProcessAsync(
            "powershell",
            [
                "-NoProfile",
                "-Command",
                $"[Windows.UI.Notifications.ToastNotificationManager, Windows.UI.Notifications, ContentType = WindowsRuntime] > $null; [Windows.Data.Xml.Dom.XmlDocument, Windows.Data.Xml.Dom.XmlDocument, ContentType = WindowsRuntime] > $null; $xml = New-Object Windows.Data.Xml.Dom.XmlDocument; $xml.LoadXml(\"<toast><visual><binding template='ToastGeneric'><text>{title}</text><text>{message}</text></binding></visual></toast>\"); $toast = [Windows.UI.Notifications.ToastNotification]::new($xml); [Windows.UI.Notifications.ToastNotificationManager]::CreateToastNotifier('Uixe.Copilot.Agent').Show($toast)"
            ],
            cancellationToken);
    }
}