using Uixe.Copilot.Application.Services;
using Uixe.Copilot.Contracts.Dtos;
using Xunit;

namespace Uixe.Copilot.Application.Tests;

public sealed class NotificationApplicationServiceTests
{
    [Fact]
    public async Task ShowWeightMessageAsync_ShouldReturnOk()
    {
        var service = new NotificationApplicationService();
        var result = await service.ShowWeightMessageAsync("6500256", new TcoWeightMessageDto(), default);

        Assert.Equal(200, result.code);
    }
}
