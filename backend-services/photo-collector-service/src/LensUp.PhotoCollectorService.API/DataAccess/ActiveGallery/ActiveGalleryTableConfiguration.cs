using LensUp.Common.AzureTableStorage.TableConfiguration;
using LensUp.Common.Types.Constants;

namespace LensUp.PhotoCollectorService.API.DataAccess.ActiveGallery;

// TODO: Shared with BackOffceService!
internal sealed class ActiveGalleryTableConfiguration : ITableConfiguration<ActiveGalleryEntity>
{
    public string TableName => TableNames.ActiveGalleries;
}
