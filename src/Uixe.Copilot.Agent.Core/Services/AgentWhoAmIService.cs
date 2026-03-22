using System.Net.Http.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Uixe.Copilot.Agent.Core.Abstractions;
using Uixe.Copilot.Agent.Core.Models;
using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Agent.Core.Services;

public sealed class AgentWhoAmIService(
    IMemoryCache cache,
    IOptions<AgentOptions> options,
    ILogger<AgentWhoAmIService> logger) : IAgentWhoAmIService
{
    private const string BossCacheKey = "agent:whoiam:boss";
    private static readonly TimeSpan BossCacheDuration = TimeSpan.FromHours(12);

#if DEBUG
    private const bool IsDebugBuild = true;
#else
    private const bool IsDebugBuild = false;
#endif

    private readonly HttpClient _httpClient = new()
    {
        Timeout = TimeSpan.FromSeconds(10)
    };

    public BossInfo? GetCachedBoss()
    {
        return cache.Get<BossInfo>(BossCacheKey);
    }

    public async Task<BossInfo?> ResolveAndCacheAsync(CancellationToken cancellationToken = default)
    {
        if (cache.TryGetValue<BossInfo>(BossCacheKey, out var cachedBoss))
        {
            return cachedBoss;
        }

        var laneBossServer = NormalizeBaseUrl(options.Value.LaneBossServer);

        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, $"{laneBossServer}guesswhoiam");
            using var response = await _httpClient.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                logger.LogWarning("Agent whoiam request failed with status code {StatusCode} from {LaneBossServer}.", response.StatusCode, laneBossServer);
                return null;
            }

            var result = await response.Content.ReadFromJsonAsync(
                AgentJsonSerializerContext.Default.LegacyBossApiResult,
                cancellationToken);

            if (result?.Code != 200 || result.Data is null)
            {
                logger.LogWarning("Agent whoiam response was empty or unsuccessful. Code={Code}, Message={Message}.", result?.Code, result?.Msg);
                return null;
            }

            var boss = MapBoss(result.Data);

            if (IsDebugBuild && options.Value.ForceLocalhostInDebugBuild)
            {
                RewriteHostsToLocalhost(boss);
            }

            cache.Set(BossCacheKey, boss, BossCacheDuration);
            return boss;
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Agent whoiam request failed against {LaneBossServer}.", laneBossServer);
            return null;
        }
    }

    private static string NormalizeBaseUrl(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return "http://127.0.0.1/";
        }

        return value.EndsWith("/", StringComparison.Ordinal) ? value : $"{value}/";
    }

    private static BossInfo MapBoss(LegacyBossDto source)
    {
        return new BossInfo
        {
            Id = source.Id ?? string.Empty,
            Name = source.Name ?? string.Empty,
            AgentIp = source.AgentIp,
            PlazaType = source.PlazaType,
            Plazas = source.Plazas?.Select(MapPlaza).ToList() ?? []
        };
    }

    private static PlazaInfo MapPlaza(LegacyPlazaDto source)
    {
        return new PlazaInfo
        {
            Id = source.Id ?? string.Empty,
            Ip = source.Ip,
            StationId = source.StationId,
            StationName = source.StationName ?? string.Empty,
            RoadId = source.RoadId,
            RoadName = source.RoadName,
            AgentIp = source.AgentIp,
            VncPassword = source.VncPwd,
            Lanes = source.Lanes?.Select(MapLane).ToList() ?? []
        };
    }

    private static LaneInfo MapLane(LegacyLaneDto source)
    {
        return new LaneInfo
        {
            Id = source.Id ?? string.Empty,
            Ip = source.Ip,
            LaneId = source.LaneId,
            LaneNo = source.LaneNo
        };
    }

    private static void RewriteHostsToLocalhost(BossInfo boss)
    {
        boss.AgentIp = "localhost";

        foreach (var plaza in boss.Plazas)
        {
            plaza.Ip = "localhost";
            plaza.AgentIp = "localhost";

            foreach (var lane in plaza.Lanes)
            {
                lane.Ip = "localhost";
            }
        }
    }
}

internal sealed class LegacyBossApiResult
{
    public int Code { get; set; }

    public string? Msg { get; set; }

    public LegacyBossDto? Data { get; set; }
}

internal sealed class LegacyBossDto
{
    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? AgentIp { get; set; }

    public int PlazaType { get; set; }

    public List<LegacyPlazaDto>? Plazas { get; set; }
}

internal sealed class LegacyPlazaDto
{
    public string? Id { get; set; }

    public string? Ip { get; set; }

    public string? RoadId { get; set; }

    public string? RoadName { get; set; }

    public string? StationId { get; set; }

    public string? StationName { get; set; }

    public string? AgentIp { get; set; }

    public string? VncPwd { get; set; }

    public List<LegacyLaneDto>? Lanes { get; set; }
}

internal sealed class LegacyLaneDto
{
    public string? Id { get; set; }

    public string? Ip { get; set; }

    public string? LaneId { get; set; }

    public string? LaneNo { get; set; }
}
