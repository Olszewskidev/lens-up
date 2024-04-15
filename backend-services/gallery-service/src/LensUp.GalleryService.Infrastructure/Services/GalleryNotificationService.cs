using LensUp.GalleryService.Application.Abstractions;
using LensUp.GalleryService.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace LensUp.GalleryService.Infrastructure.Services;

public sealed class GalleryNotificationService : IGalleryNotificationService
{
    private IHubContext<GalleryHub> galleryHub;

    public GalleryNotificationService(IHubContext<GalleryHub> galleryHub)
    {
        this.galleryHub = galleryHub;
    }

    public async Task SendPhotoUploadedToGalleryNotification(string galleryId)
    {
        await this.galleryHub.Clients.Group(galleryId).SendAsync("PhotoUploadedToGallery"); // TODO: pass payload after tests
    }
}
