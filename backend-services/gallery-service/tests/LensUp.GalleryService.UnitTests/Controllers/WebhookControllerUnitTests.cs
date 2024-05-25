using FluentAssertions;
using LensUp.GalleryService.API.Controllers;
using LensUp.GalleryService.Application.Abstractions;
using LensUp.GalleryService.Application.Models;
using LensUp.PhotoCollectorService.Contracts.Events;
using Moq;
using Xunit;

namespace LensUp.GalleryService.UnitTests.Controllers;

public sealed class WebhookControllerUnitTests
{
    private readonly Mock<IGalleryNotificationService> notificationServiceMock;
    private readonly WebhookController uut;
    public WebhookControllerUnitTests()
    {
        this.notificationServiceMock = new Mock<IGalleryNotificationService>();

        this.uut = new WebhookController(this.notificationServiceMock.Object);
    }

    [Fact]
    public async Task PhotoUploadedToGalleryHook_Should_Publish_Notification()
    {
        // Arrange
        var eventPayload = new PhotoUploadedEventPayload("photoId", "galleryId", "photoUrl", DateTimeOffset.Now, "author", "wishes");
        var @event = new PhotoUploadedEvent(eventPayload);

        PhotoUploadedNotification? photoUploadedNotification = null;
        string? galleryId = null;
        this.notificationServiceMock
            .Setup(x => x.SendPhotoUploadedToGalleryNotification(It.Is<string>(x => x == eventPayload.GalleryId), It.IsAny<PhotoUploadedNotification>()))
            .Callback<string, PhotoUploadedNotification>((x, y) =>
            {
                galleryId = x;
                photoUploadedNotification = y;
            })
            .Returns(Task.CompletedTask);

        // Act
        await this.uut.PhotoUploadedToGalleryHook(@event);

        // Assert
        this.notificationServiceMock.Verify(x => x.SendPhotoUploadedToGalleryNotification(It.Is<string>(x => x == eventPayload.GalleryId), It.IsAny<PhotoUploadedNotification>()), Times.Once);
        photoUploadedNotification.Should().NotBeNull();
        photoUploadedNotification!.Id.Should().Be(eventPayload.PhotoId);
        photoUploadedNotification.Url.Should().Be(eventPayload.PhotoUrl);
        photoUploadedNotification.AuthorName.Should().Be(eventPayload.AuthorName);
        photoUploadedNotification.WishesText.Should().Be(eventPayload.WishesText);

        galleryId.Should().Be(eventPayload.GalleryId);
    }
}
