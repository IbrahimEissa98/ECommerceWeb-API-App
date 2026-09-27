using Microsoft.Extensions.Caching.Hybrid;

namespace ECommerceApp.Infrastructure.Caching;

public static class HybridCacheExtensions
{
    private static readonly HybridCacheEntryOptions _readOnlyOptions = new()
    {
        Flags = HybridCacheEntryFlags.DisableLocalCacheWrite
            | HybridCacheEntryFlags.DisableDistributedCacheWrite
    };

    public static async ValueTask<T?> TryGetAsync<T>(
        this HybridCache cache,
        string key,
        CancellationToken ct = default)
        where T : class
    {
        var found = true;

        var value = await cache.GetOrCreateAsync(
            key,
            _ =>
            {
                found = false;
                return ValueTask.FromResult<T?>(default);
            },
            _readOnlyOptions,
            cancellationToken: ct);

        return found ? value : default;
    }
}
