using LensUp.Common.AzureTableStorage.Repository;
using LensUp.GalleryService.Domain.Entities;
using LensUp.GalleryService.Domain.Repositories;

namespace LensUp.GalleryService.Infrastructure.Repositories;

public sealed class ActiveGalleryReadOnlyRepository : IActiveGalleryReadOnlyRepository
{
    private readonly ITableClientRepository<ActiveGalleryEntity> repostiory;

    public ActiveGalleryReadOnlyRepository(ITableClientRepository<ActiveGalleryEntity> repostiory)
    {
        this.repostiory = repostiory;
    }

    public async Task<ActiveGalleryEntity?> GetAsync(string enterCode, CancellationToken cancellationToken)
        => await this.repostiory.GetAsync(enterCode, enterCode, cancellationToken);
}
