using LensUp.PhotoCollectorService.API.Requests;

namespace LensUp.PhotoCollectorService.API.Exceptions;

public sealed class PhotoFileIsEmptyException : Exception
{
    public PhotoFileIsEmptyException() : base($"{nameof(UploadPhotoToGalleryRequest.PhotoFile)} is empty.")
    {
    }
}
