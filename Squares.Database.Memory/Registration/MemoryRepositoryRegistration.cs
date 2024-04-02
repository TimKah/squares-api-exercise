using Microsoft.Extensions.DependencyInjection;

namespace Squares.Database.Memory.Registration;

public static class MemoryRepositoryRegistration
{
    public static IServiceCollection AddMemoryRepository(this IServiceCollection serviceCollection) => 
        serviceCollection.AddSingleton<IPointsRepository, MemoryRepository>();
}