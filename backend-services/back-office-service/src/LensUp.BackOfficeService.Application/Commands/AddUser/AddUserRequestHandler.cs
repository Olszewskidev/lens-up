using LensUp.BackOfficeService.Domain.Entities;
using LensUp.BackOfficeService.Domain.Repositories;
using LensUp.Common.Types.Id;
using MediatR;

namespace LensUp.BackOfficeService.Application.Commands.AddUser;

internal sealed class AddUserRequestHandler : IRequestHandler<AddUserRequest, string>
{
    private readonly IIdGenerator idGenerator;
    private readonly IUserRepository userRepository;

    public AddUserRequestHandler(IIdGenerator idGenerator, IUserRepository userRepository)
    {
        this.idGenerator = idGenerator;
        this.userRepository = userRepository;
    }

    public async Task<string> Handle(AddUserRequest request, CancellationToken cancellationToken)
    {
        var user = UserEntity.Create(this.idGenerator.Generate(), request.Name);

        await this.userRepository.AddAsync(user, cancellationToken);

        return user.RowKey;
    }
}
