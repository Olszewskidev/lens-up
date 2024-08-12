using LensUp.BackOfficeService.Domain.Exceptions;
using LensUp.BackOfficeService.Domain.ValueObjects;
using LensUp.Common.Types.Database;
using LensUp.Common.Types.Database.ValueObjects;

namespace LensUp.BackOfficeService.Domain.Entities;

public sealed class GalleryEFEntity : IBaseEntity
{
    private GalleryEFEntity(Guid id, string userId, Name name)
    {
        this.Id = id;
        this.UserId = userId;
        this.Name = name;
        this.CreatedAt = DateTimeOffset.UtcNow;
    }

    public Guid Id { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public string UserId { get; init; }
    public Name Name { get; init; }
    public ActivationDetails? ActivationDetails { get; private set; }

    public static GalleryEFEntity Create(Guid id, string userId, Name name)
    {
        return new GalleryEFEntity(id, userId, name);
    }

    public ActivationDetails Activate(ActivationDetails activationDetails)
    {
        if (this.ActivationDetails != null)
        {
            throw new GalleryAlreadyActivatedException(this.Id.ToString());
        }

        this.ActivationDetails = activationDetails;
        return activationDetails;
    }
}
