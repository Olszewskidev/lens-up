using Azure.Storage.Queues;
using LensUp.Common.Types.Events;
using System.Text.Json;

namespace LensUp.Common.AzureQueueStorage;

public abstract class BaseAzureQueueSender<T> where T : IEventMessage
{
    private readonly QueueClient client;

    public abstract string QueueName { get; }

    public BaseAzureQueueSender(QueueServiceClient queueServiceClient)
    {
        this.client = queueServiceClient.GetQueueClient(this.QueueName);
    }

    public async Task SendAsync(T eventMessage)
    {
        var eventMessageJson = JsonSerializer.Serialize(eventMessage);
        await this.client.SendMessageAsync(eventMessageJson);
    }
}
