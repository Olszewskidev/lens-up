using LensUp.BackOfficeService.Domain.Entities;

namespace LensUp.BackOfficeService.Domain.Repositories;

public interface IGalleryRepository
{
    Task AddAsync(GalleryEntity gallery, CancellationToken cancellationToken);

    Task<GalleryEntity> GetAsync(string id, CancellationToken cancellationToken);

    Task UpdateAsync(GalleryEntity gallery, CancellationToken cancellationToken);
}
