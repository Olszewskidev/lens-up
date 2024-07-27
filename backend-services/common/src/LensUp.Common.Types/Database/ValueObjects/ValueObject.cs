namespace LensUp.Common.Types.Database.ValueObjects;

public abstract class ValueObject : IEquatable<ValueObject>
{
    protected abstract IEnumerable<object?> GetAtomicValues();

    public bool Equals(ValueObject? other)
    {
        return other is not null && this.ValuesAreEqual(other);
    }

    public override bool Equals(object? obj)
    {
        return obj is ValueObject other && this.ValuesAreEqual(other);
    }

    public override int GetHashCode()
    {
        return this.GetAtomicValues()
            .Aggregate(default(int), HashCode.Combine);
    }

    private bool ValuesAreEqual(ValueObject? other)
    {
        if (other is null)
        {
            return false;
        }

        return this.GetAtomicValues().SequenceEqual(other.GetAtomicValues());
    }
}
