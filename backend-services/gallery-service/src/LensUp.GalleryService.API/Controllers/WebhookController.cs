using LensUp.BackOfficeService.Contracts.Events;
using LensUp.GalleryService.Application.Abstractions;
using LensUp.GalleryService.Application.Models;
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
    public async Task<IActionResult> PhotoUploadedEventHook([FromBody] PhotoUploadedEvent @event)
    {
        var eventPayload = @event.Payload;
        await this.galleryNotificationService.SendPhotoUploadedToGalleryNotification(
            eventPayload.GalleryId, 
            new PhotoUploadedNotification(eventPayload.PhotoId, eventPayload.PhotoUrl, eventPayload.AuthorName, eventPayload.WishesText));

        return this.Ok();
    }

    [HttpPost]
    [Route(nameof(GalleryActivatedEvent))]
    public async Task<IActionResult> GalleryActivatedEventHook([FromBody] GalleryActivatedEvent @event)
    {
        var eventPayload = @event.Payload;

        return this.Ok();
    }
}