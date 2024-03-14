namespace LensUp.BackOfficeService.Domain.Exceptions;

public sealed class UserIsNotGalleryOwnerException : Exception
{
    public UserIsNotGalleryOwnerException(string userId) : base($"User with {userId} is not gallery owner.")
    {
    }
}
