namespace LensUp.Common.Types.Id;

public sealed class IdGenerator : IIdGenerator
{
    public string Generate() => Guid.NewGuid().ToString();
}
