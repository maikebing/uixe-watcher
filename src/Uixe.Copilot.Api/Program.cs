using Uixe.Copilot.Application;
using Uixe.Copilot.Api.Hubs;
using Uixe.Copilot.Api.Services;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Infrastructure.DependencyInjection;
using Uixe.Copilot.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<InfrastructureOptions>(builder.Configuration.GetSection(InfrastructureOptions.SectionName));
builder.Services.Configure<LocalAgentForwardingOptions>(builder.Configuration.GetSection(LocalAgentForwardingOptions.SectionName));
builder.Services.AddUixeCopilotInfrastructure();
builder.Services.AddSignalR();
builder.Services.AddScoped<IRealtimePushService, SignalRTrafficEventPushService>();
builder.Services.AddHttpClient<ILocalAgentCommandForwarder, HttpLocalAgentCommandForwarder>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.MapHub<TrafficEventsHub>("/hubs/traffic-events");
app.Run();

public partial class Program;
