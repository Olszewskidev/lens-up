using MediatR;

namespace LensUp.BackOfficeService.Application.Commands.AddGallery;

public record AddGalleryRequest(string Name) : IRequest<string>;
