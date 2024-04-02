using Microsoft.Extensions.Options;
using Squares.Configuration.Options;
using Squares.Models;

namespace Squares.Database.Redis;

public class RedisRepository : RedisRepositoryBase<string, List<Point>>, IPointsRepository
{
    public RedisRepository(IOptions<DatabaseOptions> dpOptions) : base(dpOptions)
    {
    }
    
    public async Task Add(string user, Point point)
    {
        var value = await GetAll(user);
        value.Add(point);
        await SetCache(user, value);
    }

    public async Task AddAll(string user, ICollection<Point> points)
    {
        await SetCache(user, new List<Point>(points));
    }

    public async Task Delete(string user, Point point)
    {
        var value = await GetAll(user);
        value.Remove(point);
        await SetCache(user, value);
    }

    public async Task DeleteAll(string user)
    {
        await DeleteCache(user);
    }

    public async Task<List<Point>> GetAll(string user)
    {
        return await GetCache(user);
    }
}