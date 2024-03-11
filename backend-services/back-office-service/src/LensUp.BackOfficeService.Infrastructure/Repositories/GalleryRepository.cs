using LensUp.BackOfficeService.Domain.Entities;
using LensUp.BackOfficeService.Domain.Repositories;

namespace LensUp.BackOfficeService.Infrastructure.Repositories;

public sealed class GalleryRepository : IGalleryRepository
{
    public async Task AddAsync(GalleryEntity gallery, CancellationToken cancellationToken)
        => await this.AddAsync(gallery, cancellationToken);
}
