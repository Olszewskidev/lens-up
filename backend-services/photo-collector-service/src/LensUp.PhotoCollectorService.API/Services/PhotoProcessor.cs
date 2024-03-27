using LensUp.Common.AzureBlobStorage.BlobStorage;
using LensUp.Common.Types.BlobStorage.Models;
using LensUp.Common.Types.Id;
using LensUp.PhotoCollectorService.API.DataAccess.GalleryPhoto;
using LensUp.PhotoCollectorService.API.Requests;
using LensUp.PhotoCollectorService.Contracts.Events;

namespace LensUp.PhotoCollectorService.API.Services;

public interface IPhotoProcessor
{
    Task ProcessAsync(PhotoProcessorRequest request, CancellationToken cancellationToken);
}

public sealed class PhotoProcessor : IPhotoProcessor
{
    private readonly IPhotoQueueSender queueSender;
    private readonly IBlobStorageService blobStorageService;
    private readonly IGalleryPhotoRepository galleryPhotoRepository;
    private readonly IIdGenerator idGenerator;

    public PhotoProcessor(
        IPhotoQueueSender queueSender,
        IBlobStorageService blobStorageService,
        IGalleryPhotoRepository galleryPhotoRepository,
        IIdGenerator idGenerator)
    {
        this.queueSender = queueSender;
        this.blobStorageService = blobStorageService;
        this.galleryPhotoRepository = galleryPhotoRepository;
        this.idGenerator = idGenerator;
    }

    public async Task ProcessAsync(PhotoProcessorRequest request, CancellationToken cancellationToken)
    {
        string photoId = this.idGenerator.Generate();
        var uploadedPhotoInfo = await this.UploadPhotoToBlob(photoId, request, cancellationToken);

        var galleryPhotoEntity = GalleryPhotoEntity.Create(photoId, request.GalleryId, uploadedPhotoInfo.Uri.AbsoluteUri);
        await this.galleryPhotoRepository.AddAsync(galleryPhotoEntity, cancellationToken);

        await this.queueSender.SendAsync(new PhotoUploadedEvent(new PhotoUploadedEventPayload(request.GalleryId, galleryPhotoEntity.PhotoUrl, galleryPhotoEntity.CreatedDate)));
    }

    private string CreateFileName(string id, string fileExtension)
        => $"{id}{fileExtension}";

    private async Task<UploadedPhotoInfo> UploadPhotoToBlob(string photoId, PhotoProcessorRequest request, CancellationToken cancellationToken)
    {
        string containerName = request.GalleryId;
        var photoToUpload = PhotoToUpload.Create(this.CreateFileName(photoId, request.PhotoFileExtension), request.PhotoFileByteArray);
        return await this.blobStorageService.UploadPhotoAsync(containerName, photoToUpload, cancellationToken);
    }
}
