using LensUp.BackOfficeService.Application.Commands.AddUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LensUp.BackOfficeService.API.Controllers;

[ApiController]
[Route("[controller]")]
[ApiVersion("0.1")]
public sealed class UserController : Controller
{
    private readonly IMediator mediator;

    public UserController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] AddUserRequest request)
    {
        var response = await this.mediator.Send(request);
        return this.Ok(response);
    }
}
