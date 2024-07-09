using LensUp.BackOfficeService.Contracts.Events;
using LensUp.PhotoCollectorService.Contracts.Events;

namespace LensUp.WebhooksTriggerSimulator;

internal static class Subscribers
{
    public static readonly Dictionary<string, Uri[]> PhotoQueueWebhooks = new Dictionary<string, Uri[]>()
    {
        { nameof(PhotoUploadedEvent), [new Uri("http://localhost:8082/Webhook")] }
    };

    public static readonly Dictionary<string, Uri[]> GalleryQueueWebhooks = new Dictionary<string, Uri[]>()
    {
         { nameof(GalleryActivatedEvent), [new Uri($"{Environment.GetEnvironmentVariable(AppSettingsKeys.GalleryServiceWebhookUrl)}/{nameof(GalleryActivatedEvent)}")] }
    };
}
