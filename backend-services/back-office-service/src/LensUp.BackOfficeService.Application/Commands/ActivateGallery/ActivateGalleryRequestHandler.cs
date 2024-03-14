using LensUp.BackOfficeService.Application.Abstractions;
using MediatR;

namespace LensUp.BackOfficeService.Application.Commands.ActivateGallery;

public sealed class ActivateGalleryRequestHandler : IRequestHandler<ActivateGalleryRequest, ActivateGalleryResponse>
{
    private readonly IEnterCodeGenerator enterCodeGenerator;

    public ActivateGalleryRequestHandler(IEnterCodeGenerator enterCodeGenerator)
    {
        this.enterCodeGenerator = enterCodeGenerator;
    }

    public async Task<ActivateGalleryResponse> Handle(ActivateGalleryRequest request, CancellationToken cancellationToken)
    {
        // TODO: 
        // 1 Create blob container for galler
        // 2 Generate and upload QR Code
        // 3 Update GalleryEntity

        throw new NotImplementedException();
    }
}
