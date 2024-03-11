using LensUp.BackOfficeService.Domain.Entities;
using LensUp.Common.AzureTableStorage.TableConfiguration;

namespace LensUp.BackOfficeService.Infrastructure.TableConfigurations;

internal sealed class GalleryTableConfiguration : ITableConfiguration<GalleryEntity>
{
    public string TableName => "Galleries";
}
