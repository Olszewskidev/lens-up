using LensUp.Common.AzureBlobStorage.BlobStorage;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;

namespace LensUp.Common.AzureBlobStorage;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAzureBlobStorage(this IServiceCollection services, string connectionString)
    {
        services.AddAzureClients(builder =>
        {
            builder.AddBlobServiceClient(connectionString);
        });

        services.AddScoped<IBlobStorageService, BlobStorageService>();

        return services;
    }
}
