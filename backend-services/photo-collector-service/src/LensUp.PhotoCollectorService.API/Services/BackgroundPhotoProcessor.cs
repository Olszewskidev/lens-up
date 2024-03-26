using LensUp.PhotoCollectorService.API.Channels;

namespace LensUp.PhotoCollectorService.API.Services;

public sealed class BackgroundPhotoProcessor : BackgroundService
{
    private readonly IPhotoChannel channel;
    private readonly IServiceScopeFactory serviceScopeFactory;

    public BackgroundPhotoProcessor(IPhotoChannel channel, IServiceScopeFactory serviceScopeFactory)
    {
        this.channel = channel;
        this.serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var request in this.channel.SubscribeAsync(stoppingToken)) 
        {
            try
            {
                // TODO: Read about passing FormFile to new scope
                Stream photoStream = request.PhotoFile.OpenReadStream();
                string photoFileExtension = Path.GetExtension(request.PhotoFile.FileName).ToLower();

                using (var scope = this.serviceScopeFactory.CreateScope())
                {
                    var processor = scope.ServiceProvider.GetRequiredService<IPhotoProcessor>();
                    await processor.ProcessAsync(request.GalleryId, photoStream, photoFileExtension, stoppingToken);
                }
            }
            catch (Exception)
            {
                // TODO: log error
            }
        }
    }
}
