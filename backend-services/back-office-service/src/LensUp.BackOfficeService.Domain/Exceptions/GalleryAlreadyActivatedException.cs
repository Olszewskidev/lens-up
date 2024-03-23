namespace LensUp.BackOfficeService.Domain.Exceptions;

public sealed class GalleryAlreadyActivatedException : Exception
{
    public GalleryAlreadyActivatedException(string galleryId) : base($"Gallery ({galleryId}) is already activated.")
    { 
    }
}
