using LensUp.Common.Types.BlobStorage.Constants;
using LensUp.Common.Types.BlobStorage.Exceptions;
using LensUp.PhotoCollectorService.API.DataAccess.ActiveGallery;
using LensUp.PhotoCollectorService.API.Exceptions;

namespace LensUp.PhotoCollectorService.API.Validators;

public interface IUploadPhotoToGalleryRequestValidator
{
    void EnsureThatPhotoFileIsValid(IFormFile photoFile);
    Task<string> EnsureThatGalleryIsActivated(int enterCode, CancellationToken cancellationToken);
}

public sealed class UploadPhotoToGalleryRequestValidator : IUploadPhotoToGalleryRequestValidator
{
    private readonly IActiveGalleryRepository activeGalleryRepository;
    public UploadPhotoToGalleryRequestValidator(IActiveGalleryRepository activeGalleryRepository)
    {
        this.activeGalleryRepository = activeGalleryRepository;
    }

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

    public async Task<string> EnsureThatGalleryIsActivated(int enterCode, CancellationToken cancellationToken)
    {
        var activeGalleryEntity = await this.activeGalleryRepository.GetAsync(enterCode.ToString(), cancellationToken);

        if (activeGalleryEntity == null) 
        {
            throw new Exception();
        }

        if (activeGalleryEntity.EndDate < DateTimeOffset.UtcNow)
        {
            throw new Exception();
        }

        return activeGalleryEntity.GalleryId;
    }
}
