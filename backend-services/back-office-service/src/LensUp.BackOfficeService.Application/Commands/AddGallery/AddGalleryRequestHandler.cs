using LensUp.BackOfficeService.Domain.Entities;
using LensUp.BackOfficeService.Domain.Repositories;
using LensUp.Common.Types.Id;
using MediatR;

namespace LensUp.BackOfficeService.Application.Commands.AddGallery;

public sealed class AddGalleryRequestHandler : IRequestHandler<AddGalleryRequest, string>
{
    private readonly IIdGenerator idGenerator;
    private readonly IUserRepository userRepository;
    private readonly IGalleryRepository galleryRepository;

    public AddGalleryRequestHandler(IIdGenerator idGenerator, IUserRepository userRepository, IGalleryRepository galleryRepository)
    {
        this.idGenerator = idGenerator;
        this.userRepository = userRepository;
        this.galleryRepository = galleryRepository;
    }

    public async Task<string> Handle(AddGalleryRequest request, CancellationToken cancellationToken)
    {
        var gallery = await GalleryEntity.Create(this.idGenerator.Generate(), request.Name, request.UserId, this.userRepository, cancellationToken);

        await this.galleryRepository.AddAsync(gallery, cancellationToken);

        return gallery.RowKey;
    }
}
