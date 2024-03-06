using LensUp.Common.Types.Id;
using MediatR;

namespace LensUp.BackOfficeService.Application.Commands.AddUser;

internal sealed class AddUserRequestHandler : IRequestHandler<AddUserRequest, Guid>
{
    private readonly IIdGenerator idGenerator;

    public AddUserRequestHandler(IIdGenerator idGenerator)
    {
        this.idGenerator = idGenerator;
    }

    public async Task<Guid> Handle(AddUserRequest request, CancellationToken cancellationToken)
    {
        // TODO: 
        throw new NotImplementedException();
    }
}
