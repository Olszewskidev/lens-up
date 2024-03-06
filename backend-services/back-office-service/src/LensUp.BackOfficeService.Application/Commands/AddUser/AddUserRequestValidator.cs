using FluentValidation;

namespace LensUp.BackOfficeService.Application.Commands.AddUser;

public sealed class AddUserRequestValidator : AbstractValidator<AddUserRequest>
{
    public AddUserRequestValidator()
    {
        this.RuleFor(x => x).NotNull();
        this.RuleFor(x => x.Name).NotEmpty();
    }
}
