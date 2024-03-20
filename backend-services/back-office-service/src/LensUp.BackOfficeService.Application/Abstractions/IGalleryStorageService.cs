using LensUp.Common.Types.BlobStorage.Models;

namespace LensUp.BackOfficeService.Application.Abstractions;

public interface IGalleryStorageService
{
    string CreateGalleryBlobContainer(string galleryId, CancellationToken cancellationToken);

    Task<UploadedPhotoInfo> UploadQRCodeToGalleryContainer(string containerName, Stream qrCode, CancellationToken cancellationToken);
}
