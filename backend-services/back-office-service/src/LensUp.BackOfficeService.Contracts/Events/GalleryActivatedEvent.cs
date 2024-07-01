using LensUp.Common.Types.Events;

namespace LensUp.BackOfficeService.Contracts.Events;

public sealed class GalleryActivatedEvent : EventMessage<GalleryActivatedEventPayload>
{
    public GalleryActivatedEvent(GalleryActivatedEventPayload payload) : base(payload)
    {
    }
}

public sealed class GalleryActivatedEventPayload
{
    public GalleryActivatedEventPayload(string galleryId, DateTimeOffset endDate, int enterCode, string qrCodeUrl)
    {
        this.GalleryId = galleryId;
        this.EndDate = endDate;
        this.EnterCode = enterCode;
        this.QRCodeUrl = qrCodeUrl;
    }

    public string GalleryId { get; init; }

    public DateTimeOffset EndDate { get; init; }

    public int EnterCode { get; init; }

    public string QRCodeUrl { get; init; }
}
