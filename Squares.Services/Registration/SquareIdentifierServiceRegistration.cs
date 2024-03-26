using Microsoft.Extensions.DependencyInjection;
using Squares.Services.Abstractions;

namespace Squares.Services.Registration;

public static class SquareIdentifierServiceRegistration
{
    public static IServiceCollection AddSquareIdentifierService(this IServiceCollection serviceCollection) => 
        serviceCollection.AddSingleton<IShapeIdentifierService, SquareIdentifierService>();
}