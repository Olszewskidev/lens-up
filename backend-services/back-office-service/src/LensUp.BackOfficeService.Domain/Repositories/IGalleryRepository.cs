using LensUp.BackOfficeService.Domain.Entities;

namespace LensUp.BackOfficeService.Domain.Repositories;

public interface IGalleryRepository
{
    Task AddAsync(GalleryEntity gallery, CancellationToken cancellationToken);
}
