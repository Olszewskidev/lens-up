using LensUp.PhotoCollectorService.API.Requests;

namespace LensUp.PhotoCollectorService.API.Services;

public interface IPhotoProcessor
{
    Task ProcessAsync(PhotoProcessorRequest request);
}

public sealed class PhotoProcessor : IPhotoProcessor
{
    public async Task ProcessAsync(PhotoProcessorRequest request)
    {
        // 1. Upload to blob
        // 2. Upload to table
        // 3. Publish event
        await Task.Delay(1000);
    }
}
