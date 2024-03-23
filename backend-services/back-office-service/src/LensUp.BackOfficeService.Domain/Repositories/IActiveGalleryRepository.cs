using LensUp.BackOfficeService.Domain.Entities;

namespace LensUp.BackOfficeService.Domain.Repositories;

public interface IActiveGalleryRepository
{
    Task AddAsync(ActiveGalleryEntity activeGallery, CancellationToken cancellationToken);
}
