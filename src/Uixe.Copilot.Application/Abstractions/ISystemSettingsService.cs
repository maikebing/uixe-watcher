using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Application.Abstractions;

public interface ISystemSettingsService
{
    Task<SystemSettingsDto> GetAsync(CancellationToken cancellationToken = default);

    Task<SystemSettingsDto> SaveAsync(SystemSettingsDto settings, CancellationToken cancellationToken = default);
}