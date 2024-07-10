using LensUp.Common.Types.Events;
using Newtonsoft.Json.Linq;
using System.Text;

namespace LensUp.WebhooksTriggerSimulator;

public interface IEventProcessor
{
    Task Process(BinaryData messageBody, Dictionary<string, Uri[]> queueWebhooks);
}

internal sealed class EventProcessor : IEventProcessor
{
    private readonly HttpClient httpClient;

    public EventProcessor(IHttpClientFactory httpClientFactory)
    {
        this.httpClient = httpClientFactory.CreateClient(AppConstants.EventProcessorHttpClientName);
    }

    public async Task Process(BinaryData messageBody, Dictionary<string, Uri[]> queueWebhooks)
    {
        var @event = messageBody.ToString();
        if (string.IsNullOrWhiteSpace(@event))
        {
            // Log error
            return;
        }

        JObject? parsedEvent = JObject.Parse(@event);
        JToken? eventNameJToken = parsedEvent?.GetValue(nameof(EventMessage<object>.EventName), StringComparison.OrdinalIgnoreCase);
        string? eventName = eventNameJToken?.Value<string>();

        if (string.IsNullOrWhiteSpace(eventName))
        {
            // Log error
            return;
        }

        bool eventIsInQueueWebhooksDictionary = queueWebhooks.ContainsKey(eventName);
        if (!eventIsInQueueWebhooksDictionary)
        {
            // Log error
            return;
        }

        var webHooks = queueWebhooks[eventName];
        bool hasWebHooks = webHooks.Any();
        if (!hasWebHooks)
        {
            // Log info
            return;
        }

        var content = new StringContent(@event, Encoding.UTF8, "application/json");
        foreach (var webHook in webHooks)
        {
            await this.httpClient.PostAsync(webHook.AbsoluteUri, content);
        }
    }
}
