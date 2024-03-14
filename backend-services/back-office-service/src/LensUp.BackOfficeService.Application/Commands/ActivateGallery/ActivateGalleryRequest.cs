using MediatR;

namespace LensUp.BackOfficeService.Application.Commands.ActivateGallery;

public record ActivateGalleryRequest(string GalleryId, string UserId, DateTimeOffset StartDate, DateTimeOffset EndDate) : IRequest<ActivateGalleryResponse>;
