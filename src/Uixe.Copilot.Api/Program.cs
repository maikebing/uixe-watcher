using Uixe.Copilot.Application;
using Uixe.Copilot.Api.Hubs;
using Uixe.Copilot.Api.Services;
using Uixe.Copilot.Application.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddUixeCopilotApplication();
builder.Services.AddSignalR();
builder.Services.AddScoped<IRealtimePushService, SignalRTrafficEventPushService>();

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
