using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Application.Services;

public sealed class InMemorySystemSettingsService : ISystemSettingsService
{
    private readonly SemaphoreSlim _lock = new(1, 1);
    private SystemSettingsDto _settings = new()
    {
        EnableVoiceBroadcast = true,
        EnableLocalNotification = true,
        EnableDarkTheme = true,
        CurrentPhase = "Phase 2",
        PhaseMilestones = new List<string>
        {
            "Phase 1：后端解耦已完成主骨架",
            "Phase 2：实时与存储持续推进",
            "Phase 3：Web 前端一期已进入联通阶段"
        }
    };

    public Task<SystemSettingsDto> GetAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Clone(_settings));
    }

    public async Task<SystemSettingsDto> SaveAsync(SystemSettingsDto settings, CancellationToken cancellationToken = default)
    {
        await _lock.WaitAsync(cancellationToken);
        try
        {
            _settings = Clone(settings);
            return Clone(_settings);
        }
        finally
        {
            _lock.Release();
        }
    }

    private static SystemSettingsDto Clone(SystemSettingsDto source)
    {
        return new SystemSettingsDto
        {
            EnableVoiceBroadcast = source.EnableVoiceBroadcast,
            EnableLocalNotification = source.EnableLocalNotification,
            EnableDarkTheme = source.EnableDarkTheme,
            CurrentPhase = source.CurrentPhase,
            PhaseMilestones = source.PhaseMilestones.ToList()
        };
    }
}