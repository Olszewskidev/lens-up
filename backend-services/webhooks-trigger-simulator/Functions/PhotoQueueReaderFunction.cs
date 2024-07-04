using Azure.Storage.Queues.Models;
using LensUp.Common.Types.Constants;
using Microsoft.Azure.Functions.Worker;

namespace LensUp.WebhooksTriggerSimulator.Functions;

public class PhotoQueueReaderFunction
{
    private readonly IEventProcessor eventProcessor;
    public PhotoQueueReaderFunction(IEventProcessor eventProcessor)
    {
        this.eventProcessor = eventProcessor;
    }

    [Function(nameof(PhotoQueueReaderFunction))]
    public async Task Run([QueueTrigger(QueueNames.PhotoQueue, Connection = AppSettingsKeys.AzureWebJobsAzureStorageConnectionString)] QueueMessage message)
    {
        if (message?.Body == null)
        {
            return;
        }

        await this.eventProcessor.Process(message.Body, Subscribers.PhotoQueueWebhooks);
    }
}
