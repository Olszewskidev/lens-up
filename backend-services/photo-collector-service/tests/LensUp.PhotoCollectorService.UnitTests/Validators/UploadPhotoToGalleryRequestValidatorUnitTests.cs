using FluentAssertions;
using LensUp.Common.Types.BlobStorage.Exceptions;
using LensUp.PhotoCollectorService.API.DataAccess.ActiveGallery;
using LensUp.PhotoCollectorService.API.Exceptions;
using LensUp.PhotoCollectorService.API.Validators;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace LensUp.PhotoCollectorService.UnitTests.Validators;

public sealed class UploadPhotoToGalleryRequestValidatorUnitTests
{
    private readonly Mock<IActiveGalleryRepository> activeGalleryRepositoryMock;

    private readonly IUploadPhotoToGalleryRequestValidator uut;
    public UploadPhotoToGalleryRequestValidatorUnitTests()
    {
        this.activeGalleryRepositoryMock = new Mock<IActiveGalleryRepository>();

        this.uut = new UploadPhotoToGalleryRequestValidator(this.activeGalleryRepositoryMock.Object);
    }

    [Fact]
    public void EnsureThatPhotoFileIsValid_Should_Throws_PhotoFileIsEmptyException_When_FileIsNull()
    {
        // Arrange
        IFormFile? formFile = null;

        // Act
        var act = () => this.uut.EnsureThatPhotoFileIsValid(formFile);

        // Assert
        act.Should().Throw<PhotoFileIsEmptyException>();
    }

    [Fact]
    public void EnsureThatPhotoFileIsValid_Should_Throws_PhotoFileIsEmptyException_When_FileIsEmpty()
    {
        // Arrange
        var formFileMock = new Mock<IFormFile>();
        formFileMock
            .Setup(x => x.Length)
            .Returns(0);

        // Act
        var act = () => this.uut.EnsureThatPhotoFileIsValid(formFileMock.Object);

        // Assert
        act.Should().Throw<PhotoFileIsEmptyException>();
    }

    [Fact]
    public void EnsureThatPhotoFileIsValid_Should_Throws_PhotoExtensionIsNotAllowedException_When_FileHasIncorrectExtension()
    {
        // Arrange
        var formFileMock = new Mock<IFormFile>();
        formFileMock
            .Setup(x => x.Length)
            .Returns(69);
        formFileMock
            .Setup(x => x.FileName)
            .Returns("file.xml");

        // Act
        var act = () => this.uut.EnsureThatPhotoFileIsValid(formFileMock.Object);

        // Assert
        act.Should().Throw<PhotoExtensionIsNotAllowedException>();
    }

    [Fact]
    public void EnsureThatPhotoFileIsValid_Should_Returns_PhotoFileExtension_When_FileIsValid()
    {
        // Arrange
        const string fileExtension = ".png";
        var formFileMock = new Mock<IFormFile>();
        formFileMock
            .Setup(x => x.Length)
            .Returns(69);
        formFileMock
            .Setup(x => x.FileName)
            .Returns($"file{fileExtension}");

        // Act
        var result = this.uut.EnsureThatPhotoFileIsValid(formFileMock.Object);

        // Assert
        result.Should().NotBeNull();
        result.Should().Be(fileExtension);
    }

    [Fact]
    public async Task EnsureThatGalleryIsActivated_Should_Throws_ActiveGalleryNotFoundException_When_ActiveGalleryIsNull()
    {
        // Arrange
        int enterCode = 12345678;
        var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token;
        ActiveGalleryEntity? activeGalleryEntity = null;

        this.activeGalleryRepositoryMock
            .Setup(x => x.GetAsync(It.Is<string>(x => x == enterCode.ToString()), It.Is<CancellationToken>(x => x == cancellationToken)))
            .ReturnsAsync(activeGalleryEntity);

        // Act
        var act = () => this.uut.EnsureThatGalleryIsActivated(enterCode, cancellationToken);

        // Assert
        await act.Should().ThrowAsync<ActiveGalleryNotFoundException>();
    }

    [Fact]
    public async Task EnsureThatGalleryIsActivated_Should_Throws_ActiveGalleryIsExpiredException_When_GalleryIsExpired()
    {
        // Arrange
        int enterCode = 12345678;
        var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token;
        ActiveGalleryEntity activeGalleryEntity = new ActiveGalleryEntity() { EndDate = DateTime.UtcNow.AddMinutes(-15)};

        this.activeGalleryRepositoryMock
            .Setup(x => x.GetAsync(It.Is<string>(x => x == enterCode.ToString()), It.Is<CancellationToken>(x => x == cancellationToken)))
            .ReturnsAsync(activeGalleryEntity);

        // Act
        var act = () => this.uut.EnsureThatGalleryIsActivated(enterCode, cancellationToken);

        // Assert
        await act.Should().ThrowAsync<ActiveGalleryIsExpiredException>();
    }

    [Fact]
    public async Task EnsureThatGalleryIsActivated_Should_Returns_GalleryId_When_EnterCodeIsValid()
    {
        // Arrange
        int enterCode = 12345678;
        var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token;

        var galleryId = Guid.NewGuid().ToString();
        ActiveGalleryEntity activeGalleryEntity = new ActiveGalleryEntity() { GalleryId = galleryId, EndDate = DateTime.UtcNow.AddMinutes(15) };

        this.activeGalleryRepositoryMock
            .Setup(x => x.GetAsync(It.Is<string>(x => x == enterCode.ToString()), It.Is<CancellationToken>(x => x == cancellationToken)))
            .ReturnsAsync(activeGalleryEntity);

        // Act
        var result = await this.uut.EnsureThatGalleryIsActivated(enterCode, cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.Should().Be(result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void EnsureThatAuthorNameIsValid_Should_Throws_ArgumentException_When_AuthorNameIsNotValid(string? authorName)
    {
        // Act
        var act = () => this.uut.EnsureThatAuthorNameIsValid(authorName);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void EnsureThatWishesTextIsValid_Should_Throws_ArgumentException_When_WishesTextIsNotValid(string? wishesText)
    {
        // Act
        var act = () => this.uut.EnsureThatWishesTextIsValid(wishesText);

        // Assert
        act.Should().Throw<ArgumentException>();
    }
}
