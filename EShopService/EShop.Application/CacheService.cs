using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application;

using Microsoft.Extensions.Caching.Memory;

public class CacheService
{
    private readonly IMemoryCache _cache;

    public CacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public string GetValue()
    {
        string key = "klucz";

        if (!_cache.TryGetValue(key, out string cachedValue))
        {
            // Wartość nie istnieje w cache – obliczamy i zapisujemy
            cachedValue = "to jest przykładowa wartość";

            var options = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromHours(24));

            _cache.Set(key, cachedValue, options);
        }

        return cachedValue;
    }
}
