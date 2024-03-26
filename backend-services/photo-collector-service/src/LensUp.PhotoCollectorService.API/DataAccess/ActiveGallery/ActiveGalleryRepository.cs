using LensUp.Common.AzureTableStorage.Repository;

namespace LensUp.PhotoCollectorService.API.DataAccess.ActiveGallery;

public interface IActiveGalleryRepository
{
    Task<ActiveGalleryEntity?> GetAsync(string enterCode, CancellationToken cancellationToken);
}

public class ActiveGalleryRepository : IActiveGalleryRepository
{
    private readonly ITableClientRepository<ActiveGalleryEntity> repostiory;

    public ActiveGalleryRepository(ITableClientRepository<ActiveGalleryEntity> repostiory)
    {
        this.repostiory = repostiory;
    }

    public async Task<ActiveGalleryEntity?> GetAsync(string enterCode, CancellationToken cancellationToken)
        => await this.repostiory.GetAsync(enterCode, enterCode, cancellationToken);
}
