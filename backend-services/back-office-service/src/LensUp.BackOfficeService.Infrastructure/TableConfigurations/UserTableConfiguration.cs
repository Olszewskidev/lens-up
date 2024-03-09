using LensUp.BackOfficeService.Domain.Entities;
using LensUp.Common.AzureTableStorage.TableConfiguration;

namespace LensUp.BackOfficeService.Infrastructure.TableConfigurations;

public sealed class UserTableConfiguration : ITableConfiguration<UserEntity>
{
    public string TableName => "Users";
}
