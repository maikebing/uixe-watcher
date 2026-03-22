using Uixe.Copilot.Agent.Core.Abstractions;
using Uixe.Copilot.Agent.Core.Models;

namespace Uixe.Copilot.Agent.Platforms.Windows.Services;

public sealed class WindowsVideoPlayerService(
    IWindowsNativeHost nativeHost,
    WindowsLibVlcDeploymentService deploymentService) : IVideoPlayerService
{
    public async Task PlayAsync(VideoPlaybackRequest request, CancellationToken cancellationToken = default)
    {
        var libVlcRoot = await deploymentService.EnsureExtractedAsync(cancellationToken);
        var escapedSource = EscapePowerShell(request.Source);
        var escapedTitle = EscapePowerShell(request.Title);
        var escapedLibVlcRoot = EscapePowerShell(libVlcRoot);
        var escapedWindowKey = EscapePowerShell(request.WindowKey ?? request.Title);
        var autoPlayCommand = request.AutoPlay
            ? "[void][LibVlcNative]::libvlc_media_player_play($player);"
            : string.Empty;

        var script = string.Join(Environment.NewLine,
            "Add-Type -AssemblyName System.Windows.Forms;",
            "Add-Type -AssemblyName System.Drawing;",
            $"$env:VLC_PLUGIN_PATH = '{escapedLibVlcRoot}\\plugins';",
            "$signature = @'",
            "using System;",
            "using System.Runtime.InteropServices;",
            string.Empty,
            "public static class LibVlcNative",
            "{",
            "    [DllImport(\"libvlc\", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]",
            "    public static extern IntPtr libvlc_new(int argc, string[] argv);",
            string.Empty,
            "    [DllImport(\"libvlc\", CallingConvention = CallingConvention.Cdecl)]",
            "    public static extern void libvlc_release(IntPtr instance);",
            string.Empty,
            "    [DllImport(\"libvlc\", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]",
            "    public static extern IntPtr libvlc_media_new_path(IntPtr instance, string path);",
            string.Empty,
            "    [DllImport(\"libvlc\", CallingConvention = CallingConvention.Cdecl)]",
            "    public static extern void libvlc_media_release(IntPtr media);",
            string.Empty,
            "    [DllImport(\"libvlc\", CallingConvention = CallingConvention.Cdecl)]",
            "    public static extern IntPtr libvlc_media_player_new_from_media(IntPtr media);",
            string.Empty,
            "    [DllImport(\"libvlc\", CallingConvention = CallingConvention.Cdecl)]",
            "    public static extern void libvlc_media_player_release(IntPtr mediaPlayer);",
            string.Empty,
            "    [DllImport(\"libvlc\", CallingConvention = CallingConvention.Cdecl)]",
            "    public static extern int libvlc_media_player_play(IntPtr mediaPlayer);",
            string.Empty,
            "    [DllImport(\"libvlc\", CallingConvention = CallingConvention.Cdecl)]",
            "    public static extern void libvlc_media_player_stop(IntPtr mediaPlayer);",
            string.Empty,
            "    [DllImport(\"libvlc\", CallingConvention = CallingConvention.Cdecl)]",
            "    public static extern void libvlc_media_player_set_hwnd(IntPtr mediaPlayer, IntPtr drawable);",
            "}",
            "'@;",
            "Add-Type -TypeDefinition $signature;",
            $"[System.Environment]::SetEnvironmentVariable('PATH', '{escapedLibVlcRoot};' + [System.Environment]::GetEnvironmentVariable('PATH'), 'Process');",
            "$form = New-Object System.Windows.Forms.Form;",
            $"$form.Text = '{escapedTitle}';",
            $"$form.Name = 'AgentVideo_{escapedWindowKey}';",
            $"$form.Width = {request.Width};",
            $"$form.Height = {request.Height};",
            "$form.StartPosition = 'CenterScreen';",
            "$existing = [System.Windows.Forms.Application]::OpenForms | Where-Object { $_.Name -eq $form.Name } | Select-Object -First 1;",
            "if ($existing -ne $null) { $existing.Close() }",
            "$panel = New-Object System.Windows.Forms.Panel;",
            "$panel.Dock = 'Fill';",
            "$panel.BackColor = [System.Drawing.Color]::Black;",
            "$form.Controls.Add($panel);",
            "$instance = [LibVlcNative]::libvlc_new(0, $null);",
            "if ($instance -eq [IntPtr]::Zero) { throw 'Failed to initialize libvlc.'; }",
            $"$media = [LibVlcNative]::libvlc_media_new_path($instance, '{escapedSource}');",
            "if ($media -eq [IntPtr]::Zero) {",
            "    [LibVlcNative]::libvlc_release($instance);",
            "    throw 'Failed to create media from source.';",
            "}",
            "$player = [LibVlcNative]::libvlc_media_player_new_from_media($media);",
            "if ($player -eq [IntPtr]::Zero) {",
            "    [LibVlcNative]::libvlc_media_release($media);",
            "    [LibVlcNative]::libvlc_release($instance);",
            "    throw 'Failed to create media player.';",
            "}",
            "$form.Add_Shown({",
            "    [LibVlcNative]::libvlc_media_player_set_hwnd($player, $panel.Handle);",
            $"    {autoPlayCommand}",
            "});",
            "$form.Add_FormClosed({",
            "    try { [LibVlcNative]::libvlc_media_player_stop($player) } catch {}",
            "    try { [LibVlcNative]::libvlc_media_player_release($player) } catch {}",
            "    try { [LibVlcNative]::libvlc_media_release($media) } catch {}",
            "    try { [LibVlcNative]::libvlc_release($instance) } catch {}",
            "});",
            "[void]$form.ShowDialog();");

        await nativeHost.LaunchProcessAsync(
            "powershell",
            ["-NoProfile", "-STA", "-ExecutionPolicy", "Bypass", "-Command", script],
            cancellationToken);
    }

    private static string EscapePowerShell(string value)
    {
        return value.Replace("'", "''");
    }
}