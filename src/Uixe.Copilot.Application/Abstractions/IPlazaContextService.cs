using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Application.Abstractions;

public interface IPlazaContextService
{
    BossInfo? GetCurrentBoss();

    void SetCurrentBoss(BossInfo boss);

    IReadOnlyCollection<PlazaInfo> GetPlazas();

    object? GetPlazaHost(string plazaId);

    void RegisterPlazaHost(string plazaId, object host);
}
