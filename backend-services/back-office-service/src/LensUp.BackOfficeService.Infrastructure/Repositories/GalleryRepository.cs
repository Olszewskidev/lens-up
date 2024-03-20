using LensUp.BackOfficeService.Domain.Entities;
using LensUp.BackOfficeService.Domain.Exceptions;
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

    public async Task<GalleryEntity> GetAsync(string id, CancellationToken cancellationToken)
    {
        var entity = await this.repostiory.GetAsync(partitionKey: id, rowKey: id, cancellationToken);

        return entity ?? throw new GalleryNotFoundException(id);
    }

    public async Task UpdateAsync(GalleryEntity gallery, CancellationToken cancellationToken)
        => await this.repostiory.UpdateAsync(gallery, cancellationToken);
}
