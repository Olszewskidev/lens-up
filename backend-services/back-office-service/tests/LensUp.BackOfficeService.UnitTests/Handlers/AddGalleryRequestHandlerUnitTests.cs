using FluentAssertions;
using LensUp.BackOfficeService.Application.Abstractions;
using LensUp.BackOfficeService.Application.Commands.AddGallery;
using LensUp.BackOfficeService.Domain.Entities;
using LensUp.BackOfficeService.Domain.Repositories;
using LensUp.Common.Types.Id;
using Moq;
using Xunit;

namespace LensUp.BackOfficeService.UnitTests.Handlers;

public sealed class AddGalleryRequestHandlerUnitTests
{
    private readonly Mock<IIdGenerator> idGeneratorMock;
    private readonly Mock<IGalleryRepository> galleryRepositoryMock;
    private readonly Mock<IUserClaims> userClaimsMock;

    private readonly AddGalleryRequestHandler uut;

    public AddGalleryRequestHandlerUnitTests()
    {
        this.idGeneratorMock = new Mock<IIdGenerator>();
        this.userClaimsMock = new Mock<IUserClaims>();
        this.galleryRepositoryMock = new Mock<IGalleryRepository>();

        this.uut = new AddGalleryRequestHandler(this.idGeneratorMock.Object, this.galleryRepositoryMock.Object, this.userClaimsMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Add_New_Gallery()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var request = new AddGalleryRequest("My Gallery");
        var cancellationToken = CancellationToken.None;
        var expectedGalleryId = Guid.NewGuid().ToString();
        GalleryEntity? addedGallery = null;

        this.idGeneratorMock.
            Setup(x => x.Generate())
            .Returns(expectedGalleryId);

        this.userClaimsMock
            .Setup(x => x.Id)
            .Returns(userId);

        this.galleryRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<GalleryEntity>(), It.Is<CancellationToken>(x => x == cancellationToken)))
            .Callback<GalleryEntity, CancellationToken>((x, _) => addedGallery = x)
            .Returns(Task.CompletedTask);

        // Act
        var result = await this.uut.Handle(request, cancellationToken);

        // Assert
        result.Should().NotBeNull().And.Be(expectedGalleryId);
        addedGallery.Should().NotBeNull();
        addedGallery!.RowKey.Should().Be(expectedGalleryId);
        addedGallery!.PartitionKey.Should().Be(userId);
        addedGallery!.Name.Should().Be(request.Name);
        addedGallery!.UserId.Should().Be(userId);

        this.galleryRepositoryMock
            .Verify(x => x.AddAsync(It.IsAny<GalleryEntity>(), It.Is<CancellationToken>(x => x == cancellationToken)), Times.Once);
    }
}
