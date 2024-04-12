using MediatR;

namespace LensUp.GalleryService.Application.Commands.LoginToGallery;

// TODO: Consider to pass enterCode more safty between FE and BE
public record LoginToGalleryRequest(int EnterCode) : IRequest<LoginToGalleryResponse>;
