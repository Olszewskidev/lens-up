using FluentAssertions;
using LensUp.BackOfficeService.Application.Commands.AddGallery;
using LensUp.BackOfficeService.Domain.Entities;
using LensUp.BackOfficeService.Domain.Exceptions;
using LensUp.BackOfficeService.Domain.Repositories;
using LensUp.Common.Types.Id;
using Moq;
using Xunit;

namespace LensUp.BackOfficeService.UnitTests.Handlers;

public sealed class AddGalleryRequestHandlerUnitTests
{
    private readonly Mock<IIdGenerator> idGeneratorMock;
    private readonly Mock<IUserRepository> userRepositoryMock;
    private readonly Mock<IGalleryRepository> galleryRepositoryMock;

    private readonly AddGalleryRequestHandler uut;

    public AddGalleryRequestHandlerUnitTests()
    {
        this.idGeneratorMock = new Mock<IIdGenerator>();
        this.userRepositoryMock = new Mock<IUserRepository>();
        this.galleryRepositoryMock = new Mock<IGalleryRepository>();

        this.uut = new AddGalleryRequestHandler(this.idGeneratorMock.Object, this.userRepositoryMock.Object, this.galleryRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Add_New_Gallery()
    {
        // Arrange
        var request = new AddGalleryRequest("My Gallery", Guid.NewGuid().ToString());
        var cancellationToken = CancellationToken.None;
        var expectedGalleryId = Guid.NewGuid().ToString();
        GalleryEntity? addedGallery = null;

        this.idGeneratorMock.
            Setup(x => x.Generate())
            .Returns(expectedGalleryId);

        this.userRepositoryMock
            .Setup(x => x.UserExists(It.IsAny<string>(), It.Is<CancellationToken>(x => x == cancellationToken)))
            .ReturnsAsync(true);

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
        addedGallery!.PartitionKey.Should().Be(expectedGalleryId);
        addedGallery!.Name.Should().Be(request.Name);
        addedGallery!.UserId.Should().Be(request.UserId);

        this.galleryRepositoryMock
            .Verify(x => x.AddAsync(It.IsAny<GalleryEntity>(), It.Is<CancellationToken>(x => x == cancellationToken)), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Throw_Exception_When_UserDoesNotExist()
    {
        // Arrange
        var request = new AddGalleryRequest("My Gallery", Guid.NewGuid().ToString());
        var cancellationToken = CancellationToken.None;

        this.idGeneratorMock.
            Setup(x => x.Generate())
            .Returns(Guid.NewGuid().ToString());

        this.userRepositoryMock
            .Setup(x => x.UserExists(It.IsAny<string>(), It.Is<CancellationToken>(x => x == cancellationToken)))
            .ReturnsAsync(false);

        // Act
        var act = () => this.uut.Handle(request, cancellationToken);

        // Assert
        await act.Should().ThrowAsync<UserNotFoundException>();
        this.galleryRepositoryMock
            .Verify(x => x.AddAsync(It.IsAny<GalleryEntity>(), It.Is<CancellationToken>(x => x == cancellationToken)), Times.Never);
    }
}
