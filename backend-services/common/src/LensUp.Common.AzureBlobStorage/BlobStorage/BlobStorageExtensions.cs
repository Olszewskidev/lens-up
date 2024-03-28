using Azure.Storage.Blobs;

namespace LensUp.Common.AzureBlobStorage.BlobStorage;

internal static class BlobStorageExtensions
{
    public static async Task UploadAsync(this BlobClient blobClient, byte[] content, CancellationToken cancellationToken)
    {
        using (MemoryStream stream = new MemoryStream(content))
        {
            await blobClient.UploadAsync(stream, cancellationToken);
        }
    }
}
