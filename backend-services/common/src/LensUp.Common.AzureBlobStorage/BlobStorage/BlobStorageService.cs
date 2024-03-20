using Azure.Storage.Blobs;
using LensUp.Common.Types.BlobStorage.Models;

namespace LensUp.Common.AzureBlobStorage.BlobStorage;

public sealed class BlobStorageService : IBlobStorageService
{
    private readonly BlobServiceClient blobServiceClient;

    public BlobStorageService(BlobServiceClient blobServiceClient)
    {
        this.blobServiceClient = blobServiceClient;
    }

    public Task CreateContainer(string containerName, CancellationToken cancellationToken)
        => this.blobServiceClient.CreateBlobContainerAsync(containerName, Azure.Storage.Blobs.Models.PublicAccessType.Blob, cancellationToken: cancellationToken);

    public async Task<UploadedPhotoInfo> UploadPhotoAsync(string containerName, PhotoToUpload photo, CancellationToken cancellationToken)
    {
        try
        {
            var blobContainerClient = this.blobServiceClient.GetBlobContainerClient(containerName);

            string blobName = photo.FileName;
            BlobClient blobClient = blobContainerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(photo.Content, cancellationToken);

            return new UploadedPhotoInfo(blobName, blobClient.Uri);
        }
        catch (Exception)
        {
            // TODO: throw more specific exceptions

            throw;
        }
    }
}
