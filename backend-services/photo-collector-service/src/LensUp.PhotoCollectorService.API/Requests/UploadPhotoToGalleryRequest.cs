namespace LensUp.PhotoCollectorService.API.Requests;

public record UploadPhotoToGalleryRequest(IFormFile File, string AuthorName, string WishesText);
