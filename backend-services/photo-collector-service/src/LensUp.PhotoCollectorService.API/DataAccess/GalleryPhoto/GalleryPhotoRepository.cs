using LensUp.Common.AzureTableStorage.Repository;

namespace LensUp.PhotoCollectorService.API.DataAccess.GalleryPhoto;

public interface IGalleryPhotoRepository
{
    Task AddAsync(GalleryPhotoEntity entity, CancellationToken cancellationToken);
}

public sealed class GalleryPhotoRepository : IGalleryPhotoRepository
{
    private readonly ITableClientRepository<GalleryPhotoEntity> repostiory;

    public GalleryPhotoRepository(ITableClientRepository<GalleryPhotoEntity> repostiory)
    {
        this.repostiory = repostiory;
    }

    public async Task AddAsync(GalleryPhotoEntity entity, CancellationToken cancellationToken)
        => await this.repostiory.AddAsync(entity, cancellationToken);
}
