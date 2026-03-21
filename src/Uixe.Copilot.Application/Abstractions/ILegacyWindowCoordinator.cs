namespace Uixe.Copilot.Application.Abstractions;

public interface ILegacyWindowCoordinator
{
    object? GetOrCreateWeightTcoWindow(string plazaId, object plazaHost, Func<object> factory);

    object? GetOrCreateTcoCallWindow(string plazaId, object plazaHost, Func<object> factory);

    bool TryEnterLaneSpecialThrottle(string laneId, object payload, TimeSpan duration);
}
