using Azure.Storage.Queues;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;

namespace LensUp.Common.AzureQueueStorage;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAzureQueue(this IServiceCollection services, string connectionString)
    {
        services.AddAzureClients(builder =>
        {
            builder.AddQueueServiceClient(connectionString)
                   .ConfigureOptions(c => c.MessageEncoding = QueueMessageEncoding.Base64);
        });

        return services;
    }
}
