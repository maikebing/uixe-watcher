using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using Uixe.Copilot.Application.Abstractions;

namespace Uixe.Watcher.Services;

public sealed class LegacyPlazaHostBootstrapService(
    IMemoryCache cache,
    IPlazaContextService plazaContextService) : ILegacyPlazaHostBootstrapService
{
    public THost GetOrCreateHost<THost>(string hostCacheKey, IEnumerable<string> plazaIds, Func<THost> factory)
        where THost : class
    {
        var host = cache.GetOrCreate(hostCacheKey, _ => factory())
            ?? throw new InvalidOperationException($"Unable to create plaza host '{hostCacheKey}'.");

        foreach (var plazaId in plazaIds.Where(static plazaId => !string.IsNullOrWhiteSpace(plazaId)))
        {
            cache.Set(BuildPlazaCacheKey(plazaId), host);
            plazaContextService.RegisterPlazaHost(plazaId, host);
        }

        return host;
    }

    private static string BuildPlazaCacheKey(string plazaId)
    {
        return $"frmPlaza_{plazaId}";
    }
}
