namespace LensUp.PhotoCollectorService.API.Exceptions;

public sealed class ActiveGalleryNotFoundException : Exception
{
    public ActiveGalleryNotFoundException(int enterCode) : base($"Active Gallery with enterCode {enterCode} was not found.")
    {
    }
}
