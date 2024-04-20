namespace LensUp.GalleryService.Application.Commands.LoginToGallery;

// TODO: Consider to return JWT token instead
public record LoginToGalleryResponse(int EnterCode, string GalleryId, string QRCodeUrl);
