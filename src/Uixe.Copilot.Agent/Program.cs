using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Uixe.Copilot.Agent;
using Uixe.Copilot.Agent.Core;
using Uixe.Copilot.Agent.Core.Models;

AppDomain.CurrentDomain.UnhandledException += (_, eventArgs) =>
{
    try
    {
        var path = Path.Combine(AppContext.BaseDirectory, "agent-fatal.log");
        File.AppendAllText(path, $"[{DateTimeOffset.Now:u}] {eventArgs.ExceptionObject}{Environment.NewLine}");
    }
    catch
    {
    }
};

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

var command = ParseCommand(args, builder.Configuration);

builder.Services.AddSingleton(command);
builder.Services.AddUixeCopilotAgentCore(builder.Configuration);
builder.Services.AddUixeCopilotAgentPlatform();

using var host = builder.Build();
await host.RunAsync();

static AgentCommand ParseCommand(string[] args, IConfiguration configuration)
{
    var keepRunning = args.Any(static arg => string.Equals(arg, "--keep-running", StringComparison.OrdinalIgnoreCase));
    var notificationTitle = GetArgument(args, "--notify-title");
    var notificationMessage = GetArgument(args, "--notify-message");
    var speakText = GetArgument(args, "--speak");
    var speakVoice = GetArgument(args, "--voice");
    var vncHost = GetArgument(args, "--vnc-host");
    var vncPort = TryParseInt(GetArgument(args, "--vnc-port"), 5900);
    var vncPassword = GetArgument(args, "--vnc-password");
    var webUrl = GetArgument(args, "--web-url") ?? configuration[$"{AgentOptions.SectionName}:WebDashboardUrl"];
    var webTitle = GetArgument(args, "--web-title") ?? "Uixe.Copilot";
    var videoSource = GetArgument(args, "--video-source");
    var videoTitle = GetArgument(args, "--video-title") ?? "Uixe.Copilot Video";
    var videoWindowKey = GetArgument(args, "--video-window-key");

    LocalNotificationRequest? notification = null;
    if (!string.IsNullOrWhiteSpace(notificationTitle) && !string.IsNullOrWhiteSpace(notificationMessage))
    {
        notification = new LocalNotificationRequest(notificationTitle, notificationMessage);
    }

    SpeechRequest? speech = null;
    if (!string.IsNullOrWhiteSpace(speakText))
    {
        speech = new SpeechRequest(speakText, speakVoice);
    }

    VncLaunchRequest? vnc = null;
    if (!string.IsNullOrWhiteSpace(vncHost))
    {
        vnc = new VncLaunchRequest(vncHost, vncPort, vncPassword);
    }

    WebViewRequest? webView = null;
    if (!string.IsNullOrWhiteSpace(webUrl))
    {
        webView = new WebViewRequest(webUrl, webTitle);
    }

    VideoPlaybackRequest? video = null;
    if (!string.IsNullOrWhiteSpace(videoSource))
    {
        video = new VideoPlaybackRequest(videoSource, videoTitle, WindowKey: videoWindowKey);
    }

    return new AgentCommand(keepRunning, notification, speech, vnc, webView, video);
}

static string? GetArgument(string[] args, string optionName)
{
    for (var index = 0; index < args.Length - 1; index++)
    {
        if (string.Equals(args[index], optionName, StringComparison.OrdinalIgnoreCase))
        {
            return args[index + 1];
        }
    }

    return null;
}

static int TryParseInt(string? value, int defaultValue)
{
    return int.TryParse(value, out var result) ? result : defaultValue;
}