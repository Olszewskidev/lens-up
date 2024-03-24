using LensUp.PhotoCollectorService.API.Requests;
using System.Threading.Channels;

namespace LensUp.PhotoCollectorService.API.Channels;

public interface IPhotoChannel
{
    ValueTask PublishAsync(UploadPhotoToGalleryRequest request);

    IAsyncEnumerable<UploadPhotoToGalleryRequest> SubscribeAsync(CancellationToken cancellationToken);
}

public sealed class PhotoChannel : IPhotoChannel
{
    private readonly Channel<UploadPhotoToGalleryRequest> channel = Channel.CreateUnbounded<UploadPhotoToGalleryRequest>();

    public ValueTask PublishAsync(UploadPhotoToGalleryRequest request) 
        => this.channel.Writer.WriteAsync(request);

    public IAsyncEnumerable<UploadPhotoToGalleryRequest> SubscribeAsync(CancellationToken cancellationToken) 
        => this.channel.Reader.ReadAllAsync(cancellationToken);
}
