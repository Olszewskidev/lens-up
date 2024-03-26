using LensUp.Common.AzureTableStorage;

namespace LensUp.PhotoCollectorService.API.DataAccess.GalleryPhoto;

public sealed class GalleryPhotoEntity : AzureTableEntityBase
{
    private GalleryPhotoEntity(string id, string galleryId, string photoUrl) : base(rowKey: id, partitionKey: galleryId)
    {
        this.GalleryId = galleryId;
        this.PhotoUrl = photoUrl;
        this.CreatedDate = DateTime.UtcNow;
    }

    public string GalleryId { get; init; }

    public string PhotoUrl { get; init; }

    public DateTimeOffset CreatedDate { get; init; }

    public static GalleryPhotoEntity Create(string id, string galleryId, string photoUrl)
        => new GalleryPhotoEntity(id, galleryId, photoUrl);
}
