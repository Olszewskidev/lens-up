namespace LensUp.GalleryService.Application.Abstractions;

public interface IGalleryNotificationService
{
    Task SendPhotoUploadedToGalleryNotification(string galleryId);
}
