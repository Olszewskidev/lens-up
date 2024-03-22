using MediatR;

namespace LensUp.BackOfficeService.Application.Commands.ActivateGallery;

public record ActivateGalleryRequest(string GalleryId, DateTimeOffset EndDate) : IRequest<ActivateGalleryResponse>;
