using LensUp.Common.AzureTableStorage.TableConfiguration;
using LensUp.Common.Types.Constants;
using LensUp.GalleryService.Domain.Entities;

namespace LensUp.GalleryService.Infrastructure.TableConfigurations;

internal sealed class ActiveGalleryTableConfiguration : ITableConfiguration<ActiveGalleryEntity>
{
    public string TableName => TableNames.ActiveGalleries;
}
