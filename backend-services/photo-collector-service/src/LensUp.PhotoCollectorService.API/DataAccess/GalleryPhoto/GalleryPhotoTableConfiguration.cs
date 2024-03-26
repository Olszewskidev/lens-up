using LensUp.Common.AzureTableStorage.TableConfiguration;

namespace LensUp.PhotoCollectorService.API.DataAccess.GalleryPhoto;

internal sealed class GalleryPhotoTableConfiguration : ITableConfiguration<GalleryPhotoEntity>
{
    public string TableName => "GalleryPhotos";
}