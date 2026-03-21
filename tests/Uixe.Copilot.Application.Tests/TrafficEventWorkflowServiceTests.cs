using Xunit;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Application.Services;
using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Application.Tests;

public sealed class TrafficEventWorkflowServiceTests
{
    [Fact]
    public async Task EnqueueAsync_ShouldMatchLane_ByLaneNoSuffix()
    {
        var service = new TrafficEventWorkflowService();
        var request = new TrafficEventPushRequestDto { LaneNo = "6501234" };
        var plazas = new[]
        {
            new PlazaInfo
            {
                Id = "P1",
                StationName = "Test",
                Lanes = new List<LaneInfo> { new() { LaneNo = "1234" } }
            }
        };

        var result = await service.EnqueueAsync(request, plazas);

        Assert.True(result);
    }
}
