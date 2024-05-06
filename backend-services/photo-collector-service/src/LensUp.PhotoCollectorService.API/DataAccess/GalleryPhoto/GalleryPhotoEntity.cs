using LensUp.Common.AzureTableStorage;

namespace LensUp.PhotoCollectorService.API.DataAccess.GalleryPhoto;

public sealed class GalleryPhotoEntity : AzureTableEntityBase
{
    private GalleryPhotoEntity(string id, string galleryId, string photoUrl, string authorName, string wishesText) : base(rowKey: id, partitionKey: galleryId)
    {
        this.GalleryId = galleryId;
        this.PhotoUrl = photoUrl;
        this.CreatedDate = DateTime.UtcNow;
        this.AuthorName = authorName;
        this.WishesText = wishesText;
    }

    public string GalleryId { get; init; }

    public string PhotoUrl { get; init; }

    public DateTimeOffset CreatedDate { get; init; }

    public string AuthorName { get; init; }

    public string WishesText { get; init; }

    public static GalleryPhotoEntity Create(string id, string galleryId, string photoUrl, string authorName, string wishesText)
        => new GalleryPhotoEntity(id, galleryId, photoUrl, authorName, wishesText);
}
