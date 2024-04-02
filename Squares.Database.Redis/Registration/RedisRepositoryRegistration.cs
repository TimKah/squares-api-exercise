using Microsoft.Extensions.DependencyInjection;

namespace Squares.Database.Redis.Registration;

public static class RedisRepositoryRegistration
{
    public static IServiceCollection AddRedisRepository(this IServiceCollection serviceCollection) =>
        serviceCollection.AddSingleton<IPointsRepository, RedisRepository>();
}