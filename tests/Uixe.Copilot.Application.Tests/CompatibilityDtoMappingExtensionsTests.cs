using Uixe.Watcher.Dtos;
using Uixe.Watcher.Services;
using Xunit;

namespace Uixe.Copilot.Application.Tests;

public sealed class CompatibilityDtoMappingExtensionsTests
{
    [Fact]
    public void ToBulkTransportDto_ShouldMapCoreFields()
    {
        var dto = new BulklyDto
        {
            Head = new Head { NetNo = "65", PlazaNo = "001", LaneID = "003" },
            SubHead = new SubHead { StaffID = "s1", StaffName = "tester" },
            VehId = "A12345",
            Alex = 6,
            Weight = 88.5f,
            Title = "bulk",
            LARGEWOODS = new LARGEWOODS { PLATE = "ÐÂA12345" }
        };

        var result = dto.ToBulkTransportDto();

        Assert.Equal("A12345", result.VehId);
        Assert.Equal("003", result.Head?.LaneId);
        Assert.Equal("tester", result.SubHead?.StaffName);
        Assert.Equal("ÐÂA12345", result.LargeWoods?.Plate);
    }

    [Fact]
    public void ToConfirmEnInfoDto_ShouldMapDerivedFields()
    {
        var dto = new ConfirmEnInfo
        {
            laneId = "6500001001",
            vehicleId = "V1",
            enStations = new List<EnStations>
            {
                new() { enStationId = "E1", mediaNo = "M1" }
            }
        };

        var result = dto.ToConfirmEnInfoDto();

        Assert.Equal("6500001", result.PlazaId);
        Assert.Equal("001", result.LaneNo);
        Assert.Single(result.EnStations);
        Assert.Equal("E1", result.EnStations[0].EnStationId);
    }
}
