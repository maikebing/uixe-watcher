using Uixe.Copilot.Application.Services;
using Uixe.Copilot.Contracts.Dtos;
using Xunit;

namespace Uixe.Copilot.Application.Tests;

public sealed class InMemoryPlazaContextServiceTests
{
    [Fact]
    public void RegisterPlazaHost_ShouldReturnRegisteredHost()
    {
        var service = new InMemoryPlazaContextService();
        var host = new object();

        service.RegisterPlazaHost("P1", host);

        Assert.Same(host, service.GetPlazaHost("P1"));
    }

    [Fact]
    public void SetCurrentBoss_ShouldExposePlazas()
    {
        var service = new InMemoryPlazaContextService();
        service.SetCurrentBoss(new BossInfo
        {
            Id = "B1",
            Name = "Boss",
            Plazas = new List<PlazaInfo>
            {
                new() { Id = "P1", StationName = "Station1" },
                new() { Id = "P2", StationName = "Station2" }
            }
        });

        var plazas = service.GetPlazas();

        Assert.Equal(2, plazas.Count);
    }
}
