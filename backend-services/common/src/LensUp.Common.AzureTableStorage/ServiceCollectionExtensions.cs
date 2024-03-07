using LensUp.Common.AzureTableStorage.Repository;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;

namespace LensUp.Common.AzureTableStorage;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAzureTables(this IServiceCollection services, string connectionString)
    {
        services.AddAzureClients(builder =>
        {
            builder.AddTableServiceClient(connectionString);
        });

        services.AddScoped(typeof(ITableClientRepository<>), typeof(TableClientRepository<>));

        return services;
    }
}
