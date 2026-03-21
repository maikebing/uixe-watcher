using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Application.Services;

public sealed class TrafficEventWorkflowService : ITrafficEventWorkflowService
{
    private readonly IPlazaContextService _plazaContextService;

    public TrafficEventWorkflowService(IPlazaContextService plazaContextService)
    {
        _plazaContextService = plazaContextService;
    }

    public Task<bool> EnqueueAsync(TrafficEventPushRequestDto request, IReadOnlyCollection<PlazaInfo> plazas, CancellationToken cancellationToken = default)
    {
        var effectivePlazas = plazas.Count == 0 ? _plazaContextService.GetPlazas() : plazas;
        if (request is null || string.IsNullOrWhiteSpace(request.LaneNo) || effectivePlazas.Count == 0)
        {
            return Task.FromResult(false);
        }

        var normalizedLane = NormalizeLaneToken(request.LaneNo);
        var matched = effectivePlazas.Any(plaza => plaza.Lanes.Any(lane => IsLaneMatch(normalizedLane, lane)));
        return Task.FromResult(matched);
    }

    private static bool IsLaneMatch(string normalizedLane, LaneInfo lane)
    {
        if (string.IsNullOrWhiteSpace(normalizedLane))
        {
            return false;
        }

        var laneNo = NormalizeLaneToken(lane.LaneNo);
        var laneId = NormalizeLaneToken(lane.LaneId);

        return TokenEquals(normalizedLane, laneNo) || TokenEquals(normalizedLane, laneId);
    }

    private static bool TokenEquals(string left, string right)
    {
        if (string.IsNullOrWhiteSpace(left) || string.IsNullOrWhiteSpace(right))
        {
            return false;
        }

        return left == right || left.EndsWith(right, StringComparison.Ordinal) || right.EndsWith(left, StringComparison.Ordinal);
    }

    private static string NormalizeLaneToken(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        return new string(value.Where(c => !char.IsWhiteSpace(c) && c != '-' && c != '_').ToArray()).ToUpperInvariant();
    }
}
