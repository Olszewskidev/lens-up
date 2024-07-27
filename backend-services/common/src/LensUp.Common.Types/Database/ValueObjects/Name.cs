using LensUp.Common.Types.Constants;
using LensUp.Common.Types.Database.Exceptions;

namespace LensUp.Common.Types.Database.ValueObjects;

public sealed class Name : ValueObject
{
    public const int MaxLength = TableColumnConstraints.NameMaxLength;

    private Name(string value)
    {
        this.Value = value;
    }

    public string Value { get; }

    public Name Create(string name)
    {
        if (name.Length > MaxLength)
        {
            throw new ValueObjectCreationException(nameof(Name), "Value is too long.");
        }

        return new Name(name);
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return this.Value;
    }
}
