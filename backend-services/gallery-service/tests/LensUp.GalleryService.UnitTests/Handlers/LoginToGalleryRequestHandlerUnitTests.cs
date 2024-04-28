using FluentAssertions;
using LensUp.GalleryService.Application.Commands.LoginToGallery;
using LensUp.GalleryService.Domain.Entities;
using LensUp.GalleryService.Domain.Exceptions;
using LensUp.GalleryService.Domain.Repositories;
using Moq;
using Xunit;

namespace LensUp.GalleryService.UnitTests.Handlers;

public sealed class LoginToGalleryRequestHandlerUnitTests
{
    private readonly LoginToGalleryRequestHandler uut;
    private readonly Mock<IActiveGalleryReadOnlyRepository> activeGalleryRepositoryMock;

    public LoginToGalleryRequestHandlerUnitTests()
    {
        this.activeGalleryRepositoryMock = new Mock<IActiveGalleryReadOnlyRepository>();
        this.uut = new LoginToGalleryRequestHandler(this.activeGalleryRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_GalleryInfo_When_EnterCode_Is_Valid()
    {
        // Arrange
        var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(2)).Token;
        var request = new LoginToGalleryRequest(1234567);
        string enterCode = request.EnterCode.ToString();
        var activeGalleryEntity = new ActiveGalleryEntity()
        { GalleryId = Guid.NewGuid().ToString(), RowKey = enterCode, PartitionKey = enterCode, QRCodeUrl = "https://my-qr.com", EndDate = DateTime.UtcNow.AddDays(1) };

        this.activeGalleryRepositoryMock
            .Setup(x => x.GetAsync(It.Is<string>(x => x == enterCode), It.Is<CancellationToken>(x => x == cancellationToken)))
            .ReturnsAsync(activeGalleryEntity);

        // Act
        var result = await this.uut.Handle(request, cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.EnterCode.Should().Be(request.EnterCode);
        result.GalleryId.Should().Be(activeGalleryEntity.GalleryId);
        result.QRCodeUrl.Should().Be(activeGalleryEntity.QRCodeUrl);
    }

    [Fact]
    public async Task Handle_Should_Throw_ActiveGallerySecurityException_When_Gallery_Is_Null()
    {
        // Arrange
        var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(2)).Token;
        var request = new LoginToGalleryRequest(1234567);
        ActiveGalleryEntity? activeGalleryEntity = null;

        this.activeGalleryRepositoryMock
            .Setup(x => x.GetAsync(It.Is<string>(x => x == request.EnterCode.ToString()), It.Is<CancellationToken>(x => x == cancellationToken)))
            .ReturnsAsync(activeGalleryEntity);

        // Act
        var act = () => this.uut.Handle(request, cancellationToken);

        // Assert
        await act.Should().ThrowAsync<ActiveGallerySecurityException>();
    }

    [Fact]
    public async Task Handle_Should_Throw_ActiveGallerySecurityException_When_Gallery_Is_Expired()
    {
        // Arrange
        var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(2)).Token;
        var request = new LoginToGalleryRequest(1234567);
        var expiredActiveGalleryEntity = new ActiveGalleryEntity() { EndDate = DateTime.UtcNow.AddDays(-1) };

        this.activeGalleryRepositoryMock
            .Setup(x => x.GetAsync(It.Is<string>(x => x == request.EnterCode.ToString()), It.Is<CancellationToken>(x => x == cancellationToken)))
            .ReturnsAsync(expiredActiveGalleryEntity);

        // Act
        var act = () => this.uut.Handle(request, cancellationToken);

        // Assert
        await act.Should().ThrowAsync<ActiveGallerySecurityException>();
    }
}
