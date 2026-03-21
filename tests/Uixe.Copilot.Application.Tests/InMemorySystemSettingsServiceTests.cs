using Uixe.Copilot.Application.Services;
using Uixe.Copilot.Contracts.Dtos;
using Xunit;

namespace Uixe.Copilot.Application.Tests;

public sealed class InMemorySystemSettingsServiceTests
{
    [Fact]
    public async Task GetAsync_ShouldReturnDefaultSettings()
    {
        var service = new InMemorySystemSettingsService();

        var settings = await service.GetAsync();

        Assert.True(settings.EnableVoiceBroadcast);
        Assert.NotEmpty(settings.PhaseMilestones);
    }

    [Fact]
    public async Task SaveAsync_ShouldPersistSettings()
    {
        var service = new InMemorySystemSettingsService();
        var saved = await service.SaveAsync(new SystemSettingsDto
        {
            EnableVoiceBroadcast = false,
            EnableLocalNotification = false,
            EnableDarkTheme = true,
            CurrentPhase = "Phase 3",
            PhaseMilestones = new List<string> { "Phase 3£∫«∞∂À…ÓªØ" }
        });

        Assert.False(saved.EnableVoiceBroadcast);
        Assert.Equal("Phase 3", saved.CurrentPhase);
    }
}