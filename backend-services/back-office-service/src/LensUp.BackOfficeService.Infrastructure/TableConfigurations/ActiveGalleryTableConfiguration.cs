using LensUp.BackOfficeService.Domain.Entities;
using LensUp.Common.AzureTableStorage.TableConfiguration;
using LensUp.Common.Types.Constants;

namespace LensUp.BackOfficeService.Infrastructure.TableConfigurations;

internal sealed class ActiveGalleryTableConfiguration : ITableConfiguration<ActiveGalleryEntity>
{
    public string TableName => TableNames.ActiveGalleries;
}
