using LensUp.Common.AzureTableStorage;

namespace LensUp.GalleryService.Domain.Entities;

// TODO: Shared with BackOffceService!
public sealed class ActiveGalleryEntity : AzureTableEntityBase
{
    public string GalleryId { get; init; } = string.Empty;

    public DateTimeOffset EndDate { get; init; }
}
