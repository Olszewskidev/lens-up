namespace LensUp.Common.Types.Database.Exceptions;

public sealed class ValueObjectCreationException : Exception
{
    public ValueObjectCreationException(string valueTypeName, string reason) : base($"Can not create ${valueTypeName} object. Reason: {reason}")
    {
    }
}
