using LensUp.BackOfficeService.Application.Commands.ActivateGallery;
using LensUp.BackOfficeService.Application.Commands.AddGallery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LensUp.BackOfficeService.API.Controllers;

[ApiController]
[Route("[controller]")]
[ApiVersion("0.1")]
public sealed class GalleryController : Controller
{
    private readonly IMediator mediator;

    public GalleryController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost(RouteVerbs.Create)]
    public async Task<IActionResult> AddGallery([FromBody] AddGalleryRequest request)
    {
        var response = await this.mediator.Send(request);
        return this.Ok(response);
    }

    [HttpPost(RouteVerbs.Activate)]
    public async Task<IActionResult> ActivateGallery([FromBody] ActivateGalleryRequest request)
    {
        var response = await this.mediator.Send(request);
        return this.Ok(response);
    }
}
