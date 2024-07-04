using Azure.Storage.Queues.Models;
using LensUp.Common.Types.Constants;
using Microsoft.Azure.Functions.Worker;

namespace LensUp.WebhooksTriggerSimulator.Functions;

public class GalleryQueueReaderFunction
{
    private readonly IEventProcessor eventProcessor;
    public GalleryQueueReaderFunction(IEventProcessor eventProcessor)
    {
        this.eventProcessor = eventProcessor;
    }

    [Function(nameof(GalleryQueueReaderFunction))]
    public async Task Run([QueueTrigger(QueueNames.GalleryQueue, Connection = AppSettingsKeys.AzureWebJobsAzureStorageConnectionString)] QueueMessage message)
    {
        if (message?.Body == null)
        {
            return;
        }

        await this.eventProcessor.Process(message.Body, Subscribers.GalleryQueueWebhooks);
    }
}
