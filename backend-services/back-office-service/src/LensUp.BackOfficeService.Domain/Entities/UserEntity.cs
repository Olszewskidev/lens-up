using LensUp.Common.AzureTableStorage;

namespace LensUp.BackOfficeService.Domain.Entities;

public sealed class UserEntity : AzureTableEntityBase
{
    public static UserEntity Create(string id, string name) => new UserEntity(id, name);

    private UserEntity(string id, string name) : base(partitionKey: id, rowKey: id)
    {
        this.Name = name;
    }

    public UserEntity()
    {
    }

    public string Name { get; init; } = string.Empty;
}
