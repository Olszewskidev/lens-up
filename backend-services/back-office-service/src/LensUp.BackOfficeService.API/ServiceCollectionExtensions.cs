namespace LensUp.BackOfficeService.API;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHealthApplicationChecks(this IServiceCollection services)
    {
        services.AddHealthChecks();
        // TODO: Add checks for the Azure Storage Account
        return services;
    }
}
