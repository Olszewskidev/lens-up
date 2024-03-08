using LensUp.BackOfficeService.Domain.Repositories;
using LensUp.BackOfficeService.Infrastructure.Repositories;
using LensUp.Common.AzureTableStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LensUp.BackOfficeService.Infrastructure;

public static class DependencyInjection
{
    private const string AzureStorageKey = "AzureStorage";
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var azureTablesConnectionString = configuration.GetConnectionString(AzureStorageKey) 
            ?? throw new Exception($"Value for key '{AzureStorageKey}' is null or not found in the configuration.");
        services.AddAzureTables(azureTablesConnectionString);

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
