using LensUp.GalleryService.Application.Abstractions;
using LensUp.PhotoCollectorService.Contracts.Events;
using Microsoft.AspNetCore.Mvc;

namespace LensUp.GalleryService.API.Controllers;

[ApiController]
[Route("[controller]")]
[ApiVersion("0.1")]
public sealed class WebhookController : Controller
{
    private readonly IGalleryNotificationService galleryNotificationService;

    public WebhookController(IGalleryNotificationService galleryNotificationService)
    {
        this.galleryNotificationService = galleryNotificationService;
    }

    [HttpPost]
    public async Task<IActionResult> PhotoUploadedToGalleryHook([FromBody] PhotoUploadedEvent @event)
    {
        await this.galleryNotificationService.SendPhotoUploadedToGalleryNotification(@event.Payload.GalleryId);
        return this.Ok();
    }
}