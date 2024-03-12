using LensUp.BackOfficeService.Domain.Entities;
using LensUp.BackOfficeService.Domain.Repositories;
using LensUp.Common.AzureTableStorage.Repository;

namespace LensUp.BackOfficeService.Infrastructure.Repositories;

public sealed class GalleryRepository : IGalleryRepository
{
    private readonly ITableClientRepository<GalleryEntity> repostiory;

    public GalleryRepository(ITableClientRepository<GalleryEntity> repostiory)
    {
        this.repostiory = repostiory;
    }

    public async Task AddAsync(GalleryEntity gallery, CancellationToken cancellationToken)
        => await this.repostiory.AddAsync(gallery, cancellationToken);
}
