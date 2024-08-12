using LensUp.Common.Types.Database.ValueObjects;

namespace LensUp.BackOfficeService.Domain.ValueObjects;

public sealed class ActivationDetails : ValueObject
{
    public ActivationDetails(int enterCode, DateTimeOffset startDate, DateTimeOffset endDate, string qRCodeUrl)
    {
        this.EnterCode = enterCode;
        this.StartDate = startDate;
        this.EndDate = endDate;
        this.QRCodeUrl = qRCodeUrl;
    }

    public DateTimeOffset StartDate { get; }

    public DateTimeOffset EndDate { get; }

    public int EnterCode { get; }

    public string QRCodeUrl { get; }

    public static ActivationDetails Create(int enterCode, DateTimeOffset startDate, DateTimeOffset endDate, string qRCodeUrl)
    {
        return new ActivationDetails(enterCode, startDate, endDate, qRCodeUrl);
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return this.EnterCode;
        yield return this.StartDate;
        yield return this.EndDate;
        yield return this.QRCodeUrl;
    }
}
