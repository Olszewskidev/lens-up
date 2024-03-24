using LensUp.PhotoCollectorService.API.Channels;

namespace LensUp.PhotoCollectorService.API.Services;

public sealed class BackgroundPhotoProcessor : BackgroundService
{
    private readonly IPhotoChannel channel;
    private readonly IPhotoProcessor processor;

    public BackgroundPhotoProcessor(IPhotoChannel channel, IPhotoProcessor processor)
    {
        this.channel = channel;
        this.processor = processor;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var request in this.channel.SubscribeAsync(stoppingToken)) 
        { 
           await this.processor.ProcessAsync(request);
        }
    }
}
