using FluentValidation;

namespace LensUp.BackOfficeService.Application.Commands.ActivateGallery;

public sealed class ActivateGalleryRequestValidator : AbstractValidator<ActivateGalleryRequest>
{
    public ActivateGalleryRequestValidator()
    {
        this.RuleFor(x => x).NotNull();
        this.RuleFor(x => x.GalleryId).NotEmpty();
        this.RuleFor(x => x.EndDate)
            .NotEmpty()
            .GreaterThan(DateTimeOffset.UtcNow);
    }
}
