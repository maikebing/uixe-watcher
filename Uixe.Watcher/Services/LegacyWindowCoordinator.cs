using Uixe.Copilot.Application.Abstractions;

namespace Uixe.Watcher.Services;

public sealed class LegacyWindowCoordinator : ILegacyWindowCoordinator
{
    private readonly Dictionary<string, object> _windows = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, DateTimeOffset> _laneSpecialThrottle = new(StringComparer.OrdinalIgnoreCase);
    private readonly object _syncRoot = new();

    public object? GetOrCreateWeightTcoWindow(string plazaId, object plazaHost, Func<object> factory)
        => GetOrCreate($"weight:{plazaId}", factory);

    public object? GetOrCreateTcoCallWindow(string plazaId, object plazaHost, Func<object> factory)
        => GetOrCreate($"tco:{plazaId}", factory);

    public bool TryEnterLaneSpecialThrottle(string laneId, object payload, TimeSpan duration)
    {
        lock (_syncRoot)
        {
            if (_laneSpecialThrottle.TryGetValue(laneId, out var expiresAt) && expiresAt > DateTimeOffset.Now)
            {
                return false;
            }

            _laneSpecialThrottle[laneId] = DateTimeOffset.Now.Add(duration);
            return true;
        }
    }

    private object GetOrCreate(string key, Func<object> factory)
    {
        lock (_syncRoot)
        {
            if (_windows.TryGetValue(key, out var existing))
            {
                return existing;
            }

            var created = factory();
            _windows[key] = created;
            return created;
        }
    }
}
