using FluentAssertions;
using LensUp.Common.AzureBlobStorage.BlobStorage;
using LensUp.Common.Types.BlobStorage.Exceptions;
using LensUp.Common.Types.BlobStorage.Models;
using LensUp.Common.Types.Id;
using LensUp.PhotoCollectorService.API.DataAccess.GalleryPhoto;
using LensUp.PhotoCollectorService.API.Requests;
using LensUp.PhotoCollectorService.API.Services;
using LensUp.PhotoCollectorService.Contracts.Events;
using Moq;
using Xunit;

namespace LensUp.PhotoCollectorService.UnitTests.Services;

public sealed class PhotoProcessorUnitTests
{
    private readonly Mock<IPhotoQueueSender> queueSenderMock;
    private readonly Mock<IBlobStorageService> blobStorageServiceMock;
    private readonly Mock<IGalleryPhotoRepository> galleryPhotoRepositoryMock;
    private readonly Mock<IIdGenerator> idGeneratorMock;

    private readonly IPhotoProcessor uut;

    public PhotoProcessorUnitTests()
    {
        this.queueSenderMock = new Mock<IPhotoQueueSender>();
        this.blobStorageServiceMock = new Mock<IBlobStorageService>();
        this.galleryPhotoRepositoryMock = new Mock<IGalleryPhotoRepository>();
        this.idGeneratorMock = new Mock<IIdGenerator>();

        this.uut = new PhotoProcessor(
            this.queueSenderMock.Object, 
            this.blobStorageServiceMock.Object, 
            this.galleryPhotoRepositoryMock.Object, 
            this.idGeneratorMock.Object);
    }

    [Fact]
    public async Task ProcessAsync_Should_Pass_When_InputIsValid()
    {
        // Arrange
        var expectedValues = new { PhotoId = Guid.NewGuid().ToString(), GalleryId = Guid.NewGuid().ToString(), PhotoUrl = "http://my-photo.com/" };
        var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token;
        const string photoFileExtension = ".png";
        byte[] photoByteArray = new byte[69];

        var request = new PhotoProcessorRequest(expectedValues.GalleryId, photoByteArray, photoFileExtension);
        GalleryPhotoEntity? addedGalleryPhotoEntity = null;
        PhotoUploadedEvent? createdEvent = null;

        this.idGeneratorMock
            .Setup(x => x.Generate())
            .Returns(expectedValues.PhotoId);

        this.blobStorageServiceMock
            .Setup(x => x.UploadPhotoAsync(It.Is<string>(x => x == expectedValues.GalleryId), It.IsAny<PhotoToUpload>(), It.Is<CancellationToken>(x => x == cancellationToken)))
            .ReturnsAsync(new UploadedPhotoInfo("blobName", new Uri(expectedValues.PhotoUrl)));

        this.galleryPhotoRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<GalleryPhotoEntity>(), It.Is<CancellationToken>(x => x == cancellationToken)))
            .Callback<GalleryPhotoEntity, CancellationToken>((x, _) => addedGalleryPhotoEntity = x)
            .Returns(Task.CompletedTask);

        this.queueSenderMock
            .Setup(x => x.SendAsync(It.IsAny<PhotoUploadedEvent>()))
            .Callback<PhotoUploadedEvent>(x => createdEvent = x)
            .Returns(Task.CompletedTask);

        // Act
        await this.uut.ProcessAsync(request, cancellationToken);

        // Assert
        this.idGeneratorMock.Verify(x => x.Generate(), Times.Once);
        this.blobStorageServiceMock.Verify(x => x.UploadPhotoAsync(It.Is<string>(x => x == expectedValues.GalleryId), It.IsAny<PhotoToUpload>(), It.Is<CancellationToken>(x => x == cancellationToken)), Times.Once);

        this.galleryPhotoRepositoryMock.Verify(x => x.AddAsync(It.IsAny<GalleryPhotoEntity>(), It.Is<CancellationToken>(x => x == cancellationToken)), Times.Once);
        addedGalleryPhotoEntity.Should().NotBeNull();
        addedGalleryPhotoEntity!.RowKey.Should().Be(expectedValues.PhotoId);
        addedGalleryPhotoEntity.PartitionKey.Should().Be(expectedValues.GalleryId);
        addedGalleryPhotoEntity.GalleryId.Should().Be(expectedValues.GalleryId);
        addedGalleryPhotoEntity.PhotoUrl.Should().Be(expectedValues.PhotoUrl);

        this.queueSenderMock.Verify(x => x.SendAsync(It.IsAny<PhotoUploadedEvent>()), Times.Once);
        createdEvent.Should().NotBeNull();
        createdEvent!.Payload.Should().NotBeNull();
        createdEvent.Payload.PhotoId.Should().Be(expectedValues.PhotoId);
        createdEvent.Payload.PhotoUrl.Should().Be(expectedValues.PhotoUrl);
        createdEvent.Payload.GalleryId.Should().Be(expectedValues.GalleryId);
    }

    [Fact]
    public async Task ProcessAsync_Should_Throws_PhotoExtensionIsNotAllowedException_When_FileExtensionIsNotValid()
    {
        // Arrange
        var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token;
        const string photoFileExtension = ".xml";
        byte[] photoByteArray = new byte[69];

        var request = new PhotoProcessorRequest(Guid.NewGuid().ToString(), photoByteArray, photoFileExtension);

        this.idGeneratorMock
            .Setup(x => x.Generate())
            .Returns(Guid.NewGuid().ToString());

        // Act
        var act = () => this.uut.ProcessAsync(request, cancellationToken);

        // Assert
        await act.Should().ThrowAsync<PhotoExtensionIsNotAllowedException>();
    }

    [Fact]
    public async Task ProcessAsync_Should_Throws_ArgumentNullException_When_ByteArrayIsNull()
    {
        // Arrange
        var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token;
        const string photoFileExtension = ".png";
        byte[]? photoByteArray = null;

        var request = new PhotoProcessorRequest(Guid.NewGuid().ToString(), photoByteArray, photoFileExtension);

        this.idGeneratorMock
            .Setup(x => x.Generate())
            .Returns(Guid.NewGuid().ToString());

        // Act
        var act = () => this.uut.ProcessAsync(request, cancellationToken);

        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }
}
