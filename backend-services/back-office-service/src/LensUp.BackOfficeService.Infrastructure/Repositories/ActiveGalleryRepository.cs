using LensUp.BackOfficeService.Domain.Entities;
using LensUp.BackOfficeService.Domain.Repositories;
using LensUp.Common.AzureTableStorage.Repository;

namespace LensUp.BackOfficeService.Infrastructure.Repositories;

public sealed class ActiveGalleryRepository : IActiveGalleryRepository
{
    private readonly ITableClientRepository<ActiveGalleryEntity> repostiory;

    public ActiveGalleryRepository(ITableClientRepository<ActiveGalleryEntity> repostiory)
    {
        this.repostiory = repostiory;
    }

    public async Task AddAsync(ActiveGalleryEntity activeGallery, CancellationToken cancellationToken)
       => await this.repostiory.AddAsync(activeGallery, cancellationToken);
}
