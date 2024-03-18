using LensUp.Common.AzureBlobStorage.Models;

namespace LensUp.Common.AzureBlobStorage.BlobStorage;

public interface IBlobStorageService
{
    Task CreateContainer(string containerName, CancellationToken cancellationToken);

    Task<UploadedPhotoInfo> UploadPhotoAsync(string containerName, PhotoToUpload photo, CancellationToken cancellationToken);
}
