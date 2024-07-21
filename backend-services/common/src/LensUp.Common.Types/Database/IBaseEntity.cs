namespace LensUp.Common.Types.Database;

public interface IBaseEntity
{
    public Guid Id { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}
