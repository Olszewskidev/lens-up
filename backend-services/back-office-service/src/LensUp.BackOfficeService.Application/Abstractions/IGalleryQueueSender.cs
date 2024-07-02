using LensUp.BackOfficeService.Contracts.Events;

namespace LensUp.BackOfficeService.Application.Abstractions;

public interface IGalleryQueueSender
{
    Task SendAsync(GalleryActivatedEvent eventMessage);
}
