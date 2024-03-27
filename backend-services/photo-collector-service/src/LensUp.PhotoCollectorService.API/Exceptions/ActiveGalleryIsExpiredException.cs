namespace LensUp.PhotoCollectorService.API.Exceptions;

public sealed class ActiveGalleryIsExpiredException : Exception
{
    public ActiveGalleryIsExpiredException(string galleryId) : base($"Gallery with Id {galleryId} is expired.")
    {
    }
}
