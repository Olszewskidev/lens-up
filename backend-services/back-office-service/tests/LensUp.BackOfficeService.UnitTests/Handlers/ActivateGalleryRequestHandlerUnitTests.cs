using FluentAssertions;
using LensUp.BackOfficeService.Application.Abstractions;
using LensUp.BackOfficeService.Application.Commands.ActivateGallery;
using LensUp.BackOfficeService.Application.Options;
using LensUp.BackOfficeService.Domain.Entities;
using LensUp.BackOfficeService.Domain.Exceptions;
using LensUp.BackOfficeService.Domain.Repositories;
using LensUp.Common.Types.BlobStorage.Models;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace LensUp.BackOfficeService.UnitTests.Handlers;

public sealed class ActivateGalleryRequestHandlerUnitTests
{
    private readonly Mock<IGalleryRepository> galleryRepositoryMock;
    private readonly Mock<IActiveGalleryRepository> activateGalleryRepositoryMock;
    private readonly Mock<IEnterCodeGenerator> enterCodeGeneratorMock;
    private readonly Mock<IQRGenerator> qrGeneratorMock;
    private readonly Mock<IGalleryStorageService> galleryStorageServiceMock;
    private readonly Mock<IUserClaims> userClaimsMock;

    private readonly ActivateGalleryRequestHandler uut;

    private const string PhotoCollectorUIUrl = "http://localhost:5002";

    public ActivateGalleryRequestHandlerUnitTests()
    {
        this.galleryRepositoryMock = new Mock<IGalleryRepository>();
        this.activateGalleryRepositoryMock = new Mock<IActiveGalleryRepository>();
        this.enterCodeGeneratorMock = new Mock<IEnterCodeGenerator>();
        this.qrGeneratorMock = new Mock<IQRGenerator>();
        this.galleryStorageServiceMock = new Mock<IGalleryStorageService>();
        this.userClaimsMock = new Mock<IUserClaims>();

        var applicationOptions = new ApplicationOptions()
        {
            PhotoCollectorUIUrl = PhotoCollectorUIUrl,
        };

        this.uut = new ActivateGalleryRequestHandler(
            this.enterCodeGeneratorMock.Object,
            this.qrGeneratorMock.Object,
            this.galleryStorageServiceMock.Object,
            this.galleryRepositoryMock.Object,
            this.activateGalleryRepositoryMock.Object,
            Options.Create<ApplicationOptions>(applicationOptions),
            this.userClaimsMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Throw_GalleryAlreadyActivatedException_When_GalleryIsAlreadyActivated()
    {
        // Arrange
        var request = new ActivateGalleryRequest(Guid.NewGuid().ToString(), DateTimeOffset.UtcNow.AddHours(2));
        var userId = Guid.NewGuid().ToString();
        var galleryEntity = GalleryEntity.Create(request.GalleryId, "Already Activated Gallery", userId);
        galleryEntity.Activate(userId, DateTimeOffset.UtcNow.AddHours(1), 1234, PhotoCollectorUIUrl);
        var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(2)).Token;

        this.userClaimsMock
            .Setup(x => x.Id)
            .Returns(userId);

        this.galleryRepositoryMock
            .Setup(x => x.GetAsync(It.Is<string>(x => x == request.GalleryId), It.Is<string>(x => x == userId), It.Is<CancellationToken>(x => x == cancellationToken)))
            .ReturnsAsync(galleryEntity);

        // Act
        var act = () => this.uut.Handle(request, cancellationToken);

        // Assert
        await act.Should().ThrowAsync<GalleryAlreadyActivatedException>();
    }

    [Fact]
    public async Task Handle_Should_ActivateGallery()
    {
        // Arrange
        var expectedValues = new { GalleryId = Guid.NewGuid().ToString(), UserId = Guid.NewGuid().ToString(), EnterCode = 1234, EndDate = DateTimeOffset.UtcNow.AddHours(2), ContainerName = "container" };
        var request = new ActivateGalleryRequest(expectedValues.GalleryId, expectedValues.EndDate);
        var galleryEntity = GalleryEntity.Create(request.GalleryId, "Gallery to activate", expectedValues.UserId);
        var uploadedPhotoInfo = new UploadedPhotoInfo("blobName", new Uri("https://my-blob-uri-test.com"));
        var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(2)).Token;

        GalleryEntity? updatedGalleryEntity = null;
        ActiveGalleryEntity? addedActiveGallery = null;

        this.userClaimsMock
            .Setup(x => x.Id)
            .Returns(expectedValues.UserId);

        this.galleryRepositoryMock
            .Setup(x => x.GetAsync(It.Is<string>(x => x == request.GalleryId), It.Is<string>(x => x == expectedValues.UserId), It.Is<CancellationToken>(x => x == cancellationToken)))
            .ReturnsAsync(galleryEntity);

        this.enterCodeGeneratorMock
            .Setup(x => x.Generate())
            .Returns(expectedValues.EnterCode);

        this.qrGeneratorMock
            .Setup(x => x.Generate(It.IsAny<Uri>()))
            .Returns(new MemoryStream());

        this.galleryStorageServiceMock
            .Setup(x => x.CreateGalleryBlobContainer(It.Is<string>(x => x == request.GalleryId), It.Is<CancellationToken>(x => x == cancellationToken)))
            .Returns(expectedValues.ContainerName);

        this.galleryStorageServiceMock
            .Setup(x => x.UploadQRCodeToGalleryContainer(It.Is<string>(x => x == expectedValues.ContainerName), It.IsAny<Stream>(), It.Is<CancellationToken>(x => x == cancellationToken)))
            .ReturnsAsync(uploadedPhotoInfo);

        this.galleryRepositoryMock
            .Setup(x => x.UpdateAsync(It.IsAny<GalleryEntity>(), It.Is<CancellationToken>(x => x == cancellationToken)))
            .Callback<GalleryEntity, CancellationToken>((x, _) => updatedGalleryEntity = x)
            .Returns(Task.CompletedTask);

        this.activateGalleryRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<ActiveGalleryEntity>(), It.Is<CancellationToken>(x => x == cancellationToken)))
            .Callback<ActiveGalleryEntity, CancellationToken>((x, _) => addedActiveGallery = x)
            .Returns(Task.CompletedTask);

        // Act
        await this.uut.Handle(request, cancellationToken);

        // Assert
        updatedGalleryEntity.Should().NotBeNull();
        updatedGalleryEntity!.StartDate.Should().NotBeNull();
        updatedGalleryEntity.EndDate.Should().Be(expectedValues.EndDate);
        updatedGalleryEntity.EnterCode.Should().Be(expectedValues.EnterCode);

        addedActiveGallery.Should().NotBeNull();
        addedActiveGallery!.RowKey.Should().Be(expectedValues.EnterCode.ToString());
        addedActiveGallery.PartitionKey.Should().Be(expectedValues.EnterCode.ToString());
        addedActiveGallery.EnterCode.Should().Be(expectedValues.EnterCode);
        addedActiveGallery.EndDate.Should().Be(expectedValues.EndDate);
        addedActiveGallery.GalleryId.Should().Be(expectedValues.GalleryId);
        addedActiveGallery.QRCodeUrl.Should().Be(uploadedPhotoInfo.Uri.AbsoluteUri);
    }
}
