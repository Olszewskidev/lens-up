using Azure.Storage.Queues;
using LensUp.Common.Types.Constants;
using Microsoft.Extensions.Hosting;

namespace LensUp.BackOfficeService.Infrastructure.Initializers;

/// <summary>
/// Azure Queue Initializer used for dev env purposes.
/// </summary>
public sealed class AzureQueueInitializer : IHostedService
{
    private readonly QueueServiceClient queueServiceClient;

    public AzureQueueInitializer(string azureStorageAccountConnectionString)
    {
        this.queueServiceClient = new QueueServiceClient(azureStorageAccountConnectionString);
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var taskList = new List<Task>();
        foreach (string queueName in QueueNames.All)
        {
            var queueClient = this.queueServiceClient.GetQueueClient(queueName);
            taskList.Add(queueClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken));
        }

        await Task.WhenAll(taskList);
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}
