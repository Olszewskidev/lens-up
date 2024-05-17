using LensUp.BackOfficeService.Application.Abstractions;
using LensUp.BackOfficeService.Application.Options;
using LensUp.BackOfficeService.Domain.Exceptions;
using LensUp.BackOfficeService.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Options;

namespace LensUp.BackOfficeService.Application.Commands.ActivateGallery;

public sealed class ActivateGalleryRequestHandler : IRequestHandler<ActivateGalleryRequest, ActivateGalleryResponse>
{
    private readonly IEnterCodeGenerator enterCodeGenerator;
    private readonly IQRGenerator qrGenerator;
    private readonly IGalleryStorageService galleryStorageService;
    private readonly IGalleryRepository galleryRepository;
    private readonly IActiveGalleryRepository activeGalleryRepository;
    private readonly IUserClaims userClaims;

    private readonly string photoCollectorUIUrl;

    public ActivateGalleryRequestHandler(
        IEnterCodeGenerator enterCodeGenerator, 
        IQRGenerator qrGenerator, 
        IGalleryStorageService galleryStorageService,
        IGalleryRepository galleryRepository,
        IActiveGalleryRepository activeGalleryRepository,
        IOptions<ApplicationOptions> applicationOptions,
        IUserClaims userClaims)
    {
        this.enterCodeGenerator = enterCodeGenerator;
        this.qrGenerator = qrGenerator;
        this.galleryStorageService = galleryStorageService;
        this.galleryRepository = galleryRepository;
        this.activeGalleryRepository = activeGalleryRepository;
        this.userClaims = userClaims;

        this.photoCollectorUIUrl = applicationOptions.Value.PhotoCollectorUIUrl;
    }

    public async Task<ActivateGalleryResponse> Handle(ActivateGalleryRequest request, CancellationToken cancellationToken)
    {
        // TODO: Check transaction possibilitty
        string userId = this.userClaims.Id;
        var galleryEntity = await this.galleryRepository.GetAsync(request.GalleryId, userId, cancellationToken);

        if (galleryEntity.EnterCode != null)
        {
            throw new GalleryAlreadyActivatedException(request.GalleryId);
        }

        int galleryEnterCode = this.enterCodeGenerator.Generate();
        var qrCode = this.qrGenerator.Generate(this.BuildGalleryUIUri(galleryEnterCode));

        string containerName = this.galleryStorageService.CreateGalleryBlobContainer(request.GalleryId, cancellationToken);
        var uploadedPhotoInfo = await this.galleryStorageService.UploadQRCodeToGalleryContainer(containerName, qrCode, cancellationToken);

        var activeGalleryEntity = galleryEntity.Activate(userId, request.EndDate, galleryEnterCode, uploadedPhotoInfo.Uri.AbsoluteUri);

        await this.galleryRepository.UpdateAsync(galleryEntity, cancellationToken);
        await this.activeGalleryRepository.AddAsync(activeGalleryEntity, cancellationToken);

        return new ActivateGalleryResponse(galleryEntity.RowKey, galleryEnterCode);
    }

    private Uri BuildGalleryUIUri(int enterCode)
        => new Uri($"{this.photoCollectorUIUrl}/{enterCode}");
}
