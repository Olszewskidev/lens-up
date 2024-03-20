using LensUp.BackOfficeService.Application.Abstractions;
using LensUp.BackOfficeService.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace LensUp.BackOfficeService.Application.Commands.ActivateGallery;

public sealed class ActivateGalleryRequestHandler : IRequestHandler<ActivateGalleryRequest, ActivateGalleryResponse>
{
    private readonly IEnterCodeGenerator enterCodeGenerator;
    private readonly IQRGenerator qrGenerator;
    private readonly IGalleryStorageService galleryStorageService;
    private readonly IGalleryRepository galleryRepository;

    private readonly string galleryUIUrl;

    public ActivateGalleryRequestHandler(
        IEnterCodeGenerator enterCodeGenerator, 
        IQRGenerator qrGenerator, 
        IGalleryStorageService galleryStorageService,
        IGalleryRepository galleryRepository,
        IConfiguration configuration)
    {
        this.enterCodeGenerator = enterCodeGenerator;
        this.qrGenerator = qrGenerator;
        this.galleryStorageService = galleryStorageService;
        this.galleryRepository = galleryRepository;

        this.galleryUIUrl = configuration.GetValue<string>("GalleryUIUrl") ?? throw new ArgumentNullException(); // TODO: refactor
    }

    public async Task<ActivateGalleryResponse> Handle(ActivateGalleryRequest request, CancellationToken cancellationToken)
    {
        // TODO: Check transaction possibilitty
        var galleryEntity = await this.galleryRepository.GetAsync(request.GalleryId, cancellationToken);

        var galleryEnterCode = this.enterCodeGenerator.Generate();
        var qrCode = this.qrGenerator.Generate(this.BuildGalleryUIUri(galleryEntity.RowKey, galleryEnterCode));

        var containerName = this.galleryStorageService.CreateGalleryBlobContainer(request.GalleryId, cancellationToken);
        var uploadedPhotoInfo = await this.galleryStorageService.UploadQRCodeToGalleryContainer(containerName, qrCode, cancellationToken);

        galleryEntity.Activate(request.UserId, request.EndDate, galleryEnterCode, uploadedPhotoInfo.Uri.AbsoluteUri);
        await this.galleryRepository.UpdateAsync(galleryEntity, cancellationToken);

        return new ActivateGalleryResponse(galleryEntity.RowKey, galleryEnterCode);
    }

    private Uri BuildGalleryUIUri(string galleryId, int enterCode)
        => new Uri($"{this.galleryUIUrl}/{galleryId}/{enterCode}");
}
