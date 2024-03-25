using Azure.Storage.Queues;
using LensUp.Common.AzureQueueStorage;
using LensUp.PhotoCollectorService.Contracts.Events;

namespace LensUp.PhotoCollectorService.API.Services;

public interface IPhotoQueueSender
{
    Task SendAsync(PhotoUploadedEvent eventMessage);
}

public sealed class PhotoQueueSender : BaseAzureQueueSender<PhotoUploadedEvent>, IPhotoQueueSender
{
    public PhotoQueueSender(QueueServiceClient queueServiceClient) : base(queueServiceClient)
    {
    }

    public override string QueueName => "photo-queue";
}
