using Azure.Storage.Queues.Models;
using LensUp.PhotoCollectorService.Contracts.Events;
using Microsoft.Azure.Functions.Worker;
using System.Text;
using System.Text.Json;

namespace LensUp.GalleryService.WebhookTriggerSimulator;

// Workaround - simulate webhook trigger
public sealed class WebhookTriggerSimulatorFunction
{
    private const string WebhookUrl = "https://localhost:7018/Webhook";
    public WebhookTriggerSimulatorFunction()
    {
    }

    [Function(nameof(WebhookTriggerSimulatorFunction))]
    public async Task Run([QueueTrigger("photo-queue", Connection = "AzureStorageConnectionString")] QueueMessage message)
    {

        if (message?.Body == null)
        {
            return;
        }

        try
        {
            var @event = JsonSerializer.Deserialize<PhotoUploadedEvent>(message.Body);
            if (@event == null)
            {
                return;
            }

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using var client = new HttpClient(clientHandler);

            var content = new StringContent(JsonSerializer.Serialize(@event), Encoding.UTF8, "application/json");
            await client.PostAsync(WebhookUrl, content);
        }
        catch (Exception exception)
        {
            throw;
        }
    }
}
