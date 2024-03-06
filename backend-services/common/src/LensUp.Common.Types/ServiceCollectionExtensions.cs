using LensUp.Common.Types.Id;
using Microsoft.Extensions.DependencyInjection;

namespace LensUp.Common.Types;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdGenerator(this IServiceCollection services)
    {
        services.AddScoped<IIdGenerator, IdGenerator>();
        return services;
    }
}
