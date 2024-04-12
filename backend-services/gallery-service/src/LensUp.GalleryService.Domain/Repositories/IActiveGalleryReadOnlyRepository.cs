using LensUp.GalleryService.Domain.Entities;

namespace LensUp.GalleryService.Domain.Repositories;

public interface IActiveGalleryReadOnlyRepository
{
    Task<ActiveGalleryEntity?> GetAsync(string enterCode, CancellationToken cancellationToken);
}
