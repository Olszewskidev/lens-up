using LensUp.Common.AzureTableStorage.Repository;
using LensUp.Common.AzureTableStorage.TableConfiguration;
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

        return services;
    }

    public static IServiceCollection AddAzureTableRepository<TEntity>(
        this IServiceCollection services,
        ITableConfiguration<TEntity> tableConfiguration)
        where TEntity : AzureTableEntityBase
    {
        services.AddSingleton(tableConfiguration);
        services.AddScoped(typeof(ITableClientRepository<TEntity>), typeof(TableClientRepository<TEntity>));

        return services;
    }
}
