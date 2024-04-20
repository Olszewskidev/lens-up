using LensUp.Common.AzureTableStorage;

namespace LensUp.BackOfficeService.Domain.Entities;

public sealed class ActiveGalleryEntity : AzureTableEntityBase
{
    private ActiveGalleryEntity(int enterCode, string galleryId, DateTimeOffset endDate, string qrCodeUrl) : base(partitionKey: enterCode.ToString(), rowKey: enterCode.ToString())
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

    internal static ActiveGalleryEntity Create(int enterCode, string galleryId, DateTimeOffset endDate, string qrCodeUrl)
    {
        return new ActiveGalleryEntity(enterCode, galleryId, endDate, qrCodeUrl);
    }
}
