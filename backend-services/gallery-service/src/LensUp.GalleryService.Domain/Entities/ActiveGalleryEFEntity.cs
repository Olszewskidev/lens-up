using LensUp.Common.Types.Database;

namespace LensUp.GalleryService.Domain.Entities;

public sealed class ActiveGalleryEFEntity : IBaseEntity
{
    private ActiveGalleryEFEntity(Guid id, int enterCode, DateTimeOffset startDate, DateTimeOffset endDate, string qRCodeUrl)
    {
        this.Id = id;
        this.EnterCode = enterCode;
        this.StartDate = startDate;
        this.EndDate = endDate;
        this.QRCodeUrl = qRCodeUrl;
        this.CreatedAt = DateTimeOffset.UtcNow;
    }

    public Guid Id { get; init; }
    public int EnterCode { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset StartDate { get; init; }
    public DateTimeOffset EndDate { get; init; }
    public string QRCodeUrl { get; init; }

    public static ActiveGalleryEFEntity Create(Guid id, int enterCode, DateTimeOffset startDate, DateTimeOffset endDate, string qRCodeUrl)
    {
        return new ActiveGalleryEFEntity(id, enterCode, startDate, endDate, qRCodeUrl);
    }
}
