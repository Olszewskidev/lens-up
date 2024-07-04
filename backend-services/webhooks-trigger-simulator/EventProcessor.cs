namespace LensUp.WebhooksTriggerSimulator;

public interface IEventProcessor
{
    Task Process(BinaryData messageBody, Dictionary<string, Uri[]> queueWebhooks);
}

internal sealed class EventProcessor : IEventProcessor
{
    public async Task Process(BinaryData messageBody, Dictionary<string, Uri[]> queueWebhooks)
    {
        var @event = messageBody.ToString();
        if (string.IsNullOrWhiteSpace(@event))
        {
            return;
        }

        // TODO: Call webhooks
    }
}
