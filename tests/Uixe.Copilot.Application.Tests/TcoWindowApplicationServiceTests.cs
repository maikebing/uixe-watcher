using Uixe.Copilot.Application.Services;
using Xunit;

namespace Uixe.Copilot.Application.Tests;

public sealed class TcoWindowApplicationServiceTests
{
    [Fact]
    public async Task ShowWeightMessageAsync_ShouldReturnOk()
    {
        var service = new TcoWindowApplicationService();
        var result = await service.ShowWeightMessageAsync("6500256", new { }, default);

        Assert.Equal(200, result.code);
    }

    [Fact]
    public async Task ShowTcoConfirmAsync_ShouldReturnOk()
    {
        var service = new TcoWindowApplicationService();
        var result = await service.ShowTcoConfirmAsync("6500256", new { }, default);

        Assert.Equal(200, result.code);
    }
}
