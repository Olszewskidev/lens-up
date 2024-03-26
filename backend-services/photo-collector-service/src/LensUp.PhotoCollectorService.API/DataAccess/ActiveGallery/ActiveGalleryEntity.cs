using LensUp.Common.AzureTableStorage;

namespace LensUp.PhotoCollectorService.API.DataAccess.ActiveGallery;

// TODO: Shared with BackOffceService!
public sealed class ActiveGalleryEntity : AzureTableEntityBase
{
    public string GalleryId { get; init; } = string.Empty;

    public DateTimeOffset EndDate { get; init; }
}
