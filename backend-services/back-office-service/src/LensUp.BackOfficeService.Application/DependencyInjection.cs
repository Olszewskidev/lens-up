using FluentValidation;
using LensUp.Common.Types;
using Microsoft.Extensions.DependencyInjection;

namespace LensUp.BackOfficeService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration => 
            configuration.RegisterServicesFromAssemblies(
                typeof(DependencyInjection).Assembly));

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddIdGenerator();

        return services;
    }
}
