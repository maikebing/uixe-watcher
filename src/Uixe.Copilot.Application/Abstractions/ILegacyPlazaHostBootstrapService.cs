namespace Uixe.Copilot.Application.Abstractions;

public interface ILegacyPlazaHostBootstrapService
{
    THost GetOrCreateHost<THost>(string hostCacheKey, IEnumerable<string> plazaIds, Func<THost> factory)
        where THost : class;
}
