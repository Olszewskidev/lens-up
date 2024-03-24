using LensUp.Common.Types.BlobStorage.Constants;
using LensUp.Common.Types.BlobStorage.Exceptions;
using LensUp.PhotoCollectorService.API.Exceptions;

namespace LensUp.PhotoCollectorService.API.Validators;

public interface IUploadPhotoToGalleryRequestValidator
{
    void EnsureThatPhotoFileIsValid(IFormFile photoFile);
    Task<string> EnsureThatGalleryIsActivated(int enterCode);
}

public sealed class UploadPhotoToGalleryRequestValidator : IUploadPhotoToGalleryRequestValidator
{
    public void EnsureThatPhotoFileIsValid(IFormFile photoFile)
    {
        if (photoFile == null || photoFile.Length == 0)
        {
            throw new PhotoFileIsEmptyException();
        }

        var extension = Path.GetExtension(photoFile.FileName).ToLower();
        if (!PhotoFileExtensions.AllowedToUpload.Contains(extension))
        {
            throw new PhotoExtensionIsNotAllowedException(extension);
        }
    }

    public async Task<string> EnsureThatGalleryIsActivated(int enterCode)
    {
        throw new NotImplementedException();

    }
}
