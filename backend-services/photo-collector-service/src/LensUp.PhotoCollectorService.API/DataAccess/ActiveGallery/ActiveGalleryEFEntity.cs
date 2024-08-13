using LensUp.Common.Types.Database;

namespace LensUp.PhotoCollectorService.API.DataAccess.ActiveGallery;

public sealed class ActiveGalleryEFEntity : IBaseEntity
{
    private ActiveGalleryEFEntity(Guid id, int enterCode, DateTimeOffset startDate, DateTimeOffset endDate)
    {
        this.Id = id;
        this.EnterCode = enterCode;
        this.CreatedAt = DateTimeOffset.UtcNow;
        this.StartDate = startDate;
        this.EndDate = endDate;
    }

    public Guid Id { get; init; }
    public int EnterCode { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset StartDate { get; init; }
    public DateTimeOffset EndDate { get; init; }

    public static ActiveGalleryEFEntity Create(Guid id, int enterCode, DateTimeOffset startDate, DateTimeOffset endDate)
    {
        return new ActiveGalleryEFEntity(id, enterCode, startDate, endDate);
    }
}
