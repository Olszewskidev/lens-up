namespace LensUp.Common.Types.Id;

public sealed class IdGenerator : IIdGenerator
{
    public Guid Generate() => Guid.NewGuid();
}
