using LensUp.PhotoCollectorService.API.Requests;

namespace LensUp.PhotoCollectorService.API.Services;

public interface IPhotoProcessor
{
    Task ProcessAsync(UploadPhotoToGalleryRequest request);
}

public sealed class PhotoProcessor : IPhotoProcessor
{
    public async Task ProcessAsync(UploadPhotoToGalleryRequest request)
    {
        await Task.Delay(2000);
    }
}
