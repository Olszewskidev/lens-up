using LensUp.PhotoCollectorService.API.Requests;
using System.Threading.Channels;

namespace LensUp.PhotoCollectorService.API.Channels;

public interface IPhotoChannel
{
    ValueTask PublishAsync(PhotoProcessorRequest request);

    IAsyncEnumerable<PhotoProcessorRequest> SubscribeAsync(CancellationToken cancellationToken);
}

public sealed class PhotoChannel : IPhotoChannel
{
    private readonly Channel<PhotoProcessorRequest> channel = Channel.CreateUnbounded<PhotoProcessorRequest>();

    public ValueTask PublishAsync(PhotoProcessorRequest request) 
        => this.channel.Writer.WriteAsync(request);

    public IAsyncEnumerable<PhotoProcessorRequest> SubscribeAsync(CancellationToken cancellationToken) 
        => this.channel.Reader.ReadAllAsync(cancellationToken);
}
