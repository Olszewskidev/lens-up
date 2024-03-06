using MediatR;

namespace LensUp.BackOfficeService.Application.Commands.AddUser;

public record AddUserRequest(string Name) : IRequest<Guid>;
