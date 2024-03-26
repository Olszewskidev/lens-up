using LensUp.Common.AzureBlobStorage.BlobStorage;
using LensUp.Common.Types.BlobStorage.Models;
using LensUp.Common.Types.Id;
using LensUp.PhotoCollectorService.API.DataAccess.GalleryPhoto;
using LensUp.PhotoCollectorService.Contracts.Events;

namespace LensUp.PhotoCollectorService.API.Services;

public interface IPhotoProcessor
{
    Task ProcessAsync(string galleryId, Stream photoStream, string photoExtension, CancellationToken cancellation);
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

    public async Task ProcessAsync(string galleryId, Stream photoStream, string photoExtension, CancellationToken cancellation)
    {
        string photoId = this.idGenerator.Generate();
        var uploadedPhotoInfo = await this.UploadPhotoToBlob(containerName: galleryId, photoId, photoExtension, photoStream, cancellation);

        var galleryPhotoEntity = GalleryPhotoEntity.Create(photoId, galleryId, uploadedPhotoInfo.Uri.AbsoluteUri);
        await this.galleryPhotoRepository.AddAsync(galleryPhotoEntity, cancellation);

        await this.queueSender.SendAsync(new PhotoUploadedEvent(new PhotoUploadedEventPayload(galleryId, galleryPhotoEntity.PhotoUrl, galleryPhotoEntity.CreatedDate)));
    }

    private string CreateFileName(string id, string fileExtension)
        => $"{id}{fileExtension}";

    private async Task<UploadedPhotoInfo> UploadPhotoToBlob(string containerName, string photoId, string photoExtension, Stream photoStream, CancellationToken cancellationToken)
    {
        var photoToUpload = PhotoToUpload.Create(this.CreateFileName(photoId, photoExtension), photoStream);
        return await this.blobStorageService.UploadPhotoAsync(containerName, photoToUpload, cancellationToken);
    }
}
