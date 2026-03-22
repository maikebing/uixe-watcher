using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Uixe.Copilot.Agent.Core.Abstractions;
using Uixe.Copilot.Agent.Core.Models;

namespace Uixe.Copilot.Agent.Core.Services;

public sealed class AgentHttpCommandHostedService(
    IOptions<AgentHttpOptions> options,
    IAgentInstructionDispatcher dispatcher) : BackgroundService
{
    private readonly HttpListener _listener = new();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _listener.Prefixes.Add(options.Value.ListenUrl);
        _listener.Start();

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var context = await _listener.GetContextAsync();
                _ = Task.Run(() => HandleAsync(context, stoppingToken), stoppingToken);
            }
        }
        catch (HttpListenerException)
        {
        }
        catch (ObjectDisposedException)
        {
        }
        finally
        {
            if (_listener.IsListening)
            {
                _listener.Stop();
            }
        }
    }

    public override void Dispose()
    {
        if (_listener.IsListening)
        {
            _listener.Stop();
        }

        _listener.Close();
        base.Dispose();
    }

    private async Task HandleAsync(HttpListenerContext context, CancellationToken cancellationToken)
    {
        if (!string.Equals(context.Request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase)
            || !string.Equals(context.Request.Url?.AbsolutePath, "/commands", StringComparison.OrdinalIgnoreCase))
        {
            await WriteAsync(context.Response, 404, new AgentInstructionResponse(false, "Not Found"), cancellationToken);
            return;
        }

        try
        {
            using var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding);
            var body = await reader.ReadToEndAsync(cancellationToken);
            var request = JsonSerializer.Deserialize(body, AgentJsonSerializerContext.Default.AgentInstructionRequest);
            if (request is null)
            {
                await WriteAsync(context.Response, 400, new AgentInstructionResponse(false, "Invalid request."), cancellationToken);
                return;
            }

            var result = await dispatcher.DispatchAsync(request, cancellationToken);
            await WriteAsync(context.Response, result.Success ? 200 : 400, result, cancellationToken);
        }
        catch (Exception ex)
        {
            await WriteAsync(context.Response, 500, new AgentInstructionResponse(false, ex.Message), cancellationToken);
        }
    }

    private static async Task WriteAsync(HttpListenerResponse response, int statusCode, AgentInstructionResponse result, CancellationToken cancellationToken)
    {
        response.StatusCode = statusCode;
        response.ContentType = "application/json";
        var json = JsonSerializer.Serialize(result, AgentJsonSerializerContext.Default.AgentInstructionResponse);
        var bytes = Encoding.UTF8.GetBytes(json);
        response.ContentLength64 = bytes.Length;
        await response.OutputStream.WriteAsync(bytes, cancellationToken);
        response.Close();
    }
}
