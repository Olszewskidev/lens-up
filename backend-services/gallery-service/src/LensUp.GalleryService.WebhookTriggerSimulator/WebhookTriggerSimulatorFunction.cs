using Azure.Storage.Queues.Models;
using LensUp.Common.Types.Constants;
using LensUp.PhotoCollectorService.Contracts.Events;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace LensUp.GalleryService.WebhookTriggerSimulator;

// Workaround - simulate webhook trigger
public sealed class WebhookTriggerSimulatorFunction
{
    private readonly string WebhookUrl = Environment.GetEnvironmentVariable(AppSettingsKeys.WebhookUrl) ?? throw new ArgumentNullException(AppSettingsKeys.WebhookUrl);
    public WebhookTriggerSimulatorFunction()
    {
    }

    [Function(nameof(WebhookTriggerSimulatorFunction))]
    public async Task Run([QueueTrigger(QueueNames.PhotoQueue, Connection = AppSettingsKeys.AzureWebJobsAzureStorageConnectionString)] QueueMessage message)
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
            await client.PostAsync(this.WebhookUrl, content);
        }
        catch (Exception exception)
        {
            throw;
        }
    }
}
