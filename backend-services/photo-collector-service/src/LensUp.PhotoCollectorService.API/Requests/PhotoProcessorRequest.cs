namespace LensUp.PhotoCollectorService.API.Requests;

public record PhotoProcessorRequest(string GalleryId, IFormFile PhotoFile);