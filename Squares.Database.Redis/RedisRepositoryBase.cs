using System.Collections.Concurrent;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Squares.Configuration.Options;
using StackExchange.Redis;

namespace Squares.Database.Redis;

public abstract class RedisRepositoryBase<TRedisKeyValue, TRedisModel>
{
    private readonly ConcurrentDictionary<string, Lazy<Task<ConnectionMultiplexer>>> _multiplexers;
    private readonly IOptions<DatabaseOptions> _dbOptions;
    
    public RedisRepositoryBase(IOptions<DatabaseOptions> dpOptions)
    {
        _dbOptions = dpOptions;
    }

    protected async Task SetCache(TRedisKeyValue key, TRedisModel value) =>
        (await Database()).StringSet(
            new RedisKey(key?.ToString()),
            new RedisValue(JsonSerializer.Serialize(value)));

    protected async Task<TRedisModel> GetCache(TRedisKeyValue key)
    {
        var redisKey = new RedisKey(key?.ToString());
        if (!(await Database()).KeyExists(redisKey)) return default;

        var value = (await Database()).StringGet(redisKey);
        if (!value.HasValue) return default;

        return JsonSerializer.Deserialize<TRedisModel>(value);
    }

    protected async Task DeleteCache(TRedisKeyValue key) =>
        (await Database()).KeyDelete(new RedisKey(key?.ToString()));

    protected async Task<ConnectionMultiplexer?> GetConnection() =>
        await _multiplexers.GetOrAdd(_dbOptions.Value.ConnectionString,
            new Lazy<Task<ConnectionMultiplexer>>(async () =>
            {
                return await ConnectionMultiplexer.ConnectAsync(_dbOptions.Value.ConnectionString);
            })).Value;

    protected async Task<IDatabase> Database()
    {
        var redis = await GetConnection() ?? throw new NullReferenceException();
        return redis.GetDatabase();
    }
}