using LensUp.BackOfficeService.Domain.Entities;
using LensUp.BackOfficeService.Domain.Repositories;
using LensUp.Common.AzureTableStorage.Repository;

namespace LensUp.BackOfficeService.Infrastructure.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly ITableClientRepository<UserEntity> repostiory;

    public UserRepository(ITableClientRepository<UserEntity> repostiory)
    {
        this.repostiory = repostiory;
    }

    public async Task AddAsync(UserEntity user, CancellationToken cancellationToken)
        => await this.repostiory.AddAsync(user, cancellationToken);

    public async Task<bool> UserExists(string userId, CancellationToken cancellationToken)
    {
        var user = await this.repostiory.GetAsync(userId, userId, cancellationToken);

        return user != null ? true : false;
    }
}
