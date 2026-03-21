using Microsoft.AspNetCore.SignalR;
using Uixe.Copilot.Api.Hubs;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Api.Services;

public sealed class SignalRTrafficEventPushService : IRealtimePushService
{
    private readonly IHubContext<TrafficEventsHub> _hubContext;

    public SignalRTrafficEventPushService(IHubContext<TrafficEventsHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task PublishTrafficEventSubmittedAsync(TrafficEventPushRequestDto request, CancellationToken cancellationToken = default)
    {
        return _hubContext.Clients.All.SendAsync("trafficEventSubmitted", new
        {
            recordId = request.RecordId,
            eventType = request.EventType,
            laneNo = request.LaneNo,
            submittedAt = DateTimeOffset.Now
        }, cancellationToken);
    }
}
