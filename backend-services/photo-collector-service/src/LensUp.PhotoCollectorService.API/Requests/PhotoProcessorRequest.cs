namespace LensUp.PhotoCollectorService.API.Requests;

public record PhotoProcessorRequest(string GalleryId, byte[] PhotoFileByteArray, string PhotoFileExtension);