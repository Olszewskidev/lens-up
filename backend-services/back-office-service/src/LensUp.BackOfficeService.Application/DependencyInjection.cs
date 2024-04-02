using FluentValidation;
using LensUp.BackOfficeService.Application.Abstractions;
using LensUp.BackOfficeService.Application.Options;
using LensUp.Common.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LensUp.BackOfficeService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationOptions(configuration);

        services.AddMediatR(configuration => 
            configuration.RegisterServicesFromAssemblies(
                typeof(DependencyInjection).Assembly));

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddIdGenerator();

        services.AddExceptionHandler();

        services.AddScoped<IUserClaims, UserClaims>();

        return services;
    }

    private static IServiceCollection AddExceptionHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }

    private static IServiceCollection AddApplicationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApplicationOptions>(
            configuration.GetSection(ApplicationOptions.Position));

        return services;
    }
}
