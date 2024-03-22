using LensUp.BackOfficeService.Application.Abstractions;
using LensUp.BackOfficeService.Domain.Entities;
using LensUp.BackOfficeService.Domain.Repositories;
using LensUp.Common.Types.Id;
using MediatR;

namespace LensUp.BackOfficeService.Application.Commands.AddGallery;

public sealed class AddGalleryRequestHandler : IRequestHandler<AddGalleryRequest, string>
{
    private readonly IIdGenerator idGenerator;
    private readonly IGalleryRepository galleryRepository;
    private readonly IUserClaims userClaims;

    public AddGalleryRequestHandler(IIdGenerator idGenerator, IGalleryRepository galleryRepository, IUserClaims userClaims)
    {
        this.idGenerator = idGenerator;
        this.galleryRepository = galleryRepository;
        this.userClaims = userClaims;
    }

    public async Task<string> Handle(AddGalleryRequest request, CancellationToken cancellationToken)
    {
        var gallery = GalleryEntity.Create(this.idGenerator.Generate(), request.Name, this.userClaims.Id);

        await this.galleryRepository.AddAsync(gallery, cancellationToken);

        return gallery.RowKey;
    }
}
