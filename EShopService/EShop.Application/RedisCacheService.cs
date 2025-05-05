using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StackExchange.Redis;
namespace EShopApplication;

public class RedisCacheService
{
    private readonly IDatabase _db;

    public RedisCacheService()
    {
        var redis = ConnectionMultiplexer.Connect("localhost:6379");
        _db = redis.GetDatabase();
    }

    public async Task<string> GetExmpleAsync()
    {
        string key = "klucz";
        string value = await _db.StringGetAsync(key);

        if (value == null)
        {
            value = "to jest przykladowa wartosc";
            await _db.StringSetAsync(key, value, TimeSpan.FromMinutes(10));
        }

        return value;
    }
}
