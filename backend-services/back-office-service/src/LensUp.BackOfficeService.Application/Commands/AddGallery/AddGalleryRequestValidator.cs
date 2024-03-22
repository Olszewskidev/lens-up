using FluentValidation;

namespace LensUp.BackOfficeService.Application.Commands.AddGallery;

public sealed class AddGalleryRequestValidator : AbstractValidator<AddGalleryRequest>
{
    public AddGalleryRequestValidator()
    {
        this.RuleFor(x => x).NotNull();
        this.RuleFor(x => x.Name).NotEmpty();
    }
}
