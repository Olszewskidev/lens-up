namespace LensUp.Common.Types.Database;

public interface IBaseEntity
{
    public Guid Id { get; init; }

    public DateTimeOffset CreatedAt { get; init; }
}
