using LensUp.BackOfficeService.Domain.Exceptions;
using LensUp.Common.AzureTableStorage;

namespace LensUp.BackOfficeService.Domain.Entities;

public sealed class GalleryEntity : AzureTableEntityBase
{
    private GalleryEntity(string id, string name, string userId) : base(partitionKey: id, rowKey: id)
    {
        this.Name = name;
        this.UserId = userId;
    }

    public GalleryEntity()
    {
    }

    public string Name { get; init; } = string.Empty;

    public string UserId { get; init; } = string.Empty;

    public DateTimeOffset? StartDate { get; private set; }

    public DateTimeOffset? EndDate { get; private set; }

    public int? EnterCode { get; private set; }

    public string? QRCodeUri { get; private set; }

    public static GalleryEntity Create(string id, string name, string userId)
    {
        return new GalleryEntity(id, name, userId);
    }

    public void Activate(string userId, DateTimeOffset endDate, int enterCode, string qrCodeUri)
    {
        bool isGalleryOwner = this.UserId == userId;
        if (!isGalleryOwner)
        {
            throw new UserIsNotGalleryOwnerException(userId);
        }

        this.StartDate = DateTimeOffset.UtcNow;
        this.EndDate = endDate;
        this.EnterCode = enterCode;
        this.QRCodeUri = qrCodeUri;
    }
}
