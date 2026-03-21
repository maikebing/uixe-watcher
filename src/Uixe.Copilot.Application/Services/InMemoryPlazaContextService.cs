using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Application.Services;

public sealed class InMemoryPlazaContextService : IPlazaContextService
{
    private readonly object _syncRoot = new();
    private readonly Dictionary<string, object> _plazaHosts = new(StringComparer.OrdinalIgnoreCase);
    private BossInfo? _currentBoss;

    public BossInfo? GetCurrentBoss()
    {
        lock (_syncRoot)
        {
            return _currentBoss;
        }
    }

    public void SetCurrentBoss(BossInfo boss)
    {
        lock (_syncRoot)
        {
            _currentBoss = boss;
        }
    }

    public IReadOnlyCollection<PlazaInfo> GetPlazas()
    {
        lock (_syncRoot)
        {
            return _currentBoss?.Plazas?.ToArray() ?? Array.Empty<PlazaInfo>();
        }
    }

    public object? GetPlazaHost(string plazaId)
    {
        lock (_syncRoot)
        {
            return _plazaHosts.TryGetValue(plazaId, out var host) ? host : null;
        }
    }

    public void RegisterPlazaHost(string plazaId, object host)
    {
        lock (_syncRoot)
        {
            _plazaHosts[plazaId] = host;
        }
    }
}
