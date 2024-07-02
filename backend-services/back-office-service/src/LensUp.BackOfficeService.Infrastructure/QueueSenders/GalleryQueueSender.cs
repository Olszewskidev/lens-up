using Azure.Storage.Queues;
using LensUp.BackOfficeService.Application.Abstractions;
using LensUp.BackOfficeService.Contracts.Events;
using LensUp.Common.AzureQueueStorage;
using LensUp.Common.Types.Constants;

namespace LensUp.BackOfficeService.Infrastructure.QueueSenders;

public sealed class GalleryQueueSender : BaseAzureQueueSender<GalleryActivatedEvent>, IGalleryQueueSender
{
    public GalleryQueueSender(QueueServiceClient queueServiceClient) : base(queueServiceClient)
    {
    }

    public override string QueueName => QueueNames.GalleryQueue;
}
