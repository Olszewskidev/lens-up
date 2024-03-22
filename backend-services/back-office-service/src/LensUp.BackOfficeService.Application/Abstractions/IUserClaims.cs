namespace LensUp.BackOfficeService.Application.Abstractions;

/// <summary>
///  Temporary solution - this interface represents claims from user jwt token.
/// </summary>
public interface IUserClaims
{
    string Id { get; }
}

public sealed class UserClaims : IUserClaims
{
    public string Id => "387185d8-4578-4426-afcf-2014b5c13710";
}
