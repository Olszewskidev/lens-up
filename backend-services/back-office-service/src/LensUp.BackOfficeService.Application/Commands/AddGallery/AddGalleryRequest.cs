using MediatR;

namespace LensUp.BackOfficeService.Application.Commands.AddGallery;

public record AddGalleryRequest(string UserId, string Name) : IRequest<string>;
