using LensUp.Common.AzureTableStorage.TableConfiguration;
using LensUp.Common.Types.Constants;

namespace LensUp.PhotoCollectorService.API.DataAccess.GalleryPhoto;

internal sealed class GalleryPhotoTableConfiguration : ITableConfiguration<GalleryPhotoEntity>
{
    public string TableName => TableNames.GalleryPhotos;
}