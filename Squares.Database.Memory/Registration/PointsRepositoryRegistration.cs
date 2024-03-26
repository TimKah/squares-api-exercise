using Microsoft.Extensions.DependencyInjection;
using Squares.Database;

namespace Squares.DTO.Memory.Registration;

public static class PointsRepositoryRegistration
{
    public static IServiceCollection AddPointsRepository(this IServiceCollection serviceCollection) => 
        serviceCollection.AddSingleton<IPointsRepository, PointsRepository>();
}