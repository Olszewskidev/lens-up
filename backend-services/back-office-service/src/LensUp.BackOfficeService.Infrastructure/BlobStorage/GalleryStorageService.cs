using LensUp.BackOfficeService.Application.Abstractions;
using LensUp.Common.AzureBlobStorage.BlobStorage;
using LensUp.Common.Types.BlobStorage.Models;

namespace LensUp.BackOfficeService.Infrastructure.BlobStorage;

public sealed class GalleryStorageService : IGalleryStorageService
{
    private readonly IBlobStorageService blobStorageService;

    public GalleryStorageService(IBlobStorageService blobStorageService)
    {
        this.blobStorageService = blobStorageService;
    }

    public string CreateGalleryBlobContainer(string galleryId, CancellationToken cancellationToken)
    {
        var containerName = galleryId;
        this.blobStorageService.CreateContainer(containerName: galleryId, cancellationToken);

        return containerName;
    }

    public async Task<UploadedPhotoInfo> UploadQRCodeToGalleryContainer(string containerName, Stream qrCodeStream, CancellationToken cancellationToken)
    {
        const string fileName = "qrCode.png";
        var photoToUpload = PhotoToUpload.Create(fileName, qrCodeStream);
        return await this.blobStorageService.UploadPhotoAsync(containerName, photoToUpload, cancellationToken);
    }
}
