using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RDSoft.OwnerCardActions.Domain.Interfaces;
using RDSoft.OwnerCardActions.Infrastructure.Config;

namespace RDSoft.OwnerCardActions.Infrastructure.Providers;

public class MemoryCacheProvider(IMemoryCache memoryCache, ILogger<MemoryCacheProvider> logger, IConfiguration configuration) : IMemoryCacheProvider
{
    private readonly CacheSettings? _cacheSettings = configuration.GetSection("CacheSettings").Get<CacheSettings>();
    
    public T Get<T>(string key)
    {
        return (memoryCache.TryGetValue(key, out T? value) ? value : default)!;
    }

    public void Set<T>(string key, T value)
    {
        var duration = _cacheSettings?.DefaultExpirationMinutes ?? 30;
        
        memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));
        logger.Log(LogLevel.Information, $"Set cache for key: {key}");
    }

    public bool TryGetValue<T>(string key, out T value)
    {
        if (memoryCache.TryGetValue(key, out T? result))
        {
            logger.Log(LogLevel.Information, $"Try/get cache succeeded for key: {key}");
            value = result!;
            return true;
        }
        value = default!;
        return false;
    }

    public void Remove(string key)
    {
        memoryCache.Remove(key);
        logger.Log(LogLevel.Information, $"Removed cache for key: {key}");
    }
}