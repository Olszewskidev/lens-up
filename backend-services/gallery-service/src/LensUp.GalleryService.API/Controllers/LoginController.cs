using LensUp.GalleryService.Application.Commands.LoginToGallery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LensUp.GalleryService.API.Controllers;

[ApiController]
[Route("[controller]")]
[ApiVersion("0.1")]
public sealed class LoginController : Controller
{
    private readonly IMediator mediator;

    public LoginController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> LoginToGallery([FromBody] LoginToGalleryRequest request)
    {
        var response = await this.mediator.Send(request);
        return this.Ok(response);
    }
}
