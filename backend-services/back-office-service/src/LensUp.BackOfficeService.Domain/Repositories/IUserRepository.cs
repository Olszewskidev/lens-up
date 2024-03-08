using LensUp.BackOfficeService.Domain.Entities;

namespace LensUp.BackOfficeService.Domain.Repositories;

public interface IUserRepository
{
    Task AddAsync(UserEntity user, CancellationToken cancellationToken);
}
