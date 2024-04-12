using LensUp.GalleryService.Domain.Exceptions;
using LensUp.GalleryService.Domain.Repositories;
using MediatR;

namespace LensUp.GalleryService.Application.Commands.LoginToGallery;

public sealed class LoginToGalleryRequestHandler : IRequestHandler<LoginToGalleryRequest, LoginToGalleryResponse>
{
    private readonly IActiveGalleryReadOnlyRepository activeGalleryRepository;

    public LoginToGalleryRequestHandler(IActiveGalleryReadOnlyRepository activeGalleryRepository)
    {
        this.activeGalleryRepository = activeGalleryRepository;
    }

    public async Task<LoginToGalleryResponse> Handle(LoginToGalleryRequest request, CancellationToken cancellationToken)
    {
        var activeGalleryEntity = await this.activeGalleryRepository.GetAsync(request.EnterCode.ToString(), cancellationToken);

        if (activeGalleryEntity == null)
        {
            throw new ActiveGallerySecurityException();
        }

        bool isExpired = activeGalleryEntity.EndDate < DateTimeOffset.UtcNow;
        if (isExpired)
        {
            throw new ActiveGallerySecurityException();
        }

        return new LoginToGalleryResponse(request.EnterCode, activeGalleryEntity.GalleryId);
    }
}
