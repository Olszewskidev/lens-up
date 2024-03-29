using LensUp.Common.Types.Events;

namespace LensUp.PhotoCollectorService.Contracts.Events;

public sealed class PhotoUploadedEvent : EventMessage<PhotoUploadedEventPayload>
{
    public PhotoUploadedEvent(PhotoUploadedEventPayload payload) : base(payload)
    {
    }
}

public sealed class PhotoUploadedEventPayload
{
    public PhotoUploadedEventPayload(string photoid, string galleryId, string photoUrl, DateTimeOffset createdDate)
    {
        this.PhotoId = photoid;
        this.GalleryId = galleryId;
        this.PhotoUrl = photoUrl;
        this.CreatedDate = createdDate;
    }

    public string PhotoId { get; init; }

    public string GalleryId { get; init; }

    public string PhotoUrl { get; init; }

    public DateTimeOffset CreatedDate { get; init; }
}
