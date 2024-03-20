namespace LensUp.BackOfficeService.Domain.Exceptions;

public sealed class GalleryNotFoundException : Exception
{
    public GalleryNotFoundException(string galleryId) : base($"Gallery with {galleryId} was not found.")
    {
    }
}
