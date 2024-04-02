namespace LensUp.BackOfficeService.Application.Options;

public sealed class ApplicationOptions
{
    public const string Position = "ApplicationOptions";

    public string GalleryUIUrl { get; init; } = string.Empty;
}
