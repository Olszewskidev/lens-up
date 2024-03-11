namespace LensUp.BackOfficeService.Domain.Exceptions;

public sealed class UserNotFoundException : Exception
{
    public UserNotFoundException(string userId) : base($"User with {userId} was not found.")
    {     
    }
}
