using LensUp.BackOfficeService.Domain.Entities;
using LensUp.Common.AzureTableStorage.TableConfiguration;

namespace LensUp.BackOfficeService.Infrastructure.TableConfigurations;

internal sealed class ActiveGalleryTableConfiguration : ITableConfiguration<ActiveGalleryEntity>
{
    public string TableName => "ActiveGalleries";
}
