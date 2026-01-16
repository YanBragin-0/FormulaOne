using System.Text.Json;
using StackExchange.Redis;

namespace FormulaOne.Infrastructure.Storage.Redis
{
    public class RedisCache(IConnectionMultiplexer multiplexer) : IRedisCache
    {
        private readonly StackExchange.Redis.IDatabase _database = multiplexer.GetDatabase();
        private IServer GetServer()
        {
            var endpoints = _database.Multiplexer.GetEndPoints();
            return _database.Multiplexer.GetServer(endpoints[0]);
        }
        private List<T> ToList<T>(T val) => val is null ? new List<T>() : new List<T> { val };

        public async Task<List<T>?> Get<T>(string key)
        {
            var json = await _database.StringGetAsync($"{(RedisKey)key}:");
            return json.HasValue ? JsonSerializer.Deserialize<List<T>>(json!) : [];
        }

        public async Task RemoveAllByPrefixAsync(string prefix)
        {
           var server = this.GetServer();
           foreach(var key in server.Keys(pattern: $"{prefix}:*"))
           {
               await _database.KeyDeleteAsync(key);
           } 
        }
        public async Task RemoveByPrefixAsync(string prefix) => await _database.KeyDeleteAsync(prefix);

        public async Task Set<T>(string key, T val, TimeSpan? expiry = null)
        {
            if (expiry is null)
            {
                expiry = TimeSpan.FromMinutes(10);
            }
            if(typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(List<>))
            {
                var json = JsonSerializer.Serialize(val);
                await _database.StringSetAsync($"{(RedisKey)key}", (RedisValue)json, (Expiration)expiry);
                return;
            }
            var ListJson = JsonSerializer.Serialize<List<T>>(this.ToList<T>(val));
            if (ListJson == null || ListJson.Length == 0) return;
            _database.StringSet((RedisKey)key,(RedisValue)ListJson,(Expiration)expiry);

        }



    }
}
