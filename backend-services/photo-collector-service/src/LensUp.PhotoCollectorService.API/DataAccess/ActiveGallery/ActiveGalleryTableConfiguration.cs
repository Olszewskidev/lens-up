using LensUp.Common.AzureTableStorage.TableConfiguration;

namespace LensUp.PhotoCollectorService.API.DataAccess.ActiveGallery;

// TODO: Shared with BackOffceService!
internal sealed class ActiveGalleryTableConfiguration : ITableConfiguration<ActiveGalleryEntity>
{
    public string TableName => "ActiveGalleries";
}
