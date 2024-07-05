using LensUp.Common.Types.Events;

namespace LensUp.PhotoCollectorService.Contracts.Events;

public sealed class PhotoUploadedEvent : EventMessage<PhotoUploadedEventPayload>
{
    public override string EventName { get; protected set; }

    public PhotoUploadedEvent(PhotoUploadedEventPayload payload) : base(payload)
    {
        this.EventName = nameof(PhotoUploadedEvent);
    }
}

public sealed class PhotoUploadedEventPayload
{
    public PhotoUploadedEventPayload(string photoid, string galleryId, string photoUrl, DateTimeOffset createdDate, string authorName, string wishesText)
    {
        this.PhotoId = photoid;
        this.GalleryId = galleryId;
        this.PhotoUrl = photoUrl;
        this.CreatedDate = createdDate;
        this.AuthorName = authorName;
        this.WishesText = wishesText;
    }

    public string PhotoId { get; init; }

    public string GalleryId { get; init; }

    public string PhotoUrl { get; init; }

    public string AuthorName { get; init; }

    public string WishesText { get; init; }

    public DateTimeOffset CreatedDate { get; init; }
}
