using LensUp.BackOfficeService.Application.Abstractions;

namespace LensUp.BackOfficeService.Infrastructure.Generators;

public sealed class EnterCodeGenerator : IEnterCodeGenerator
{
    private const int MinValue = 10000000;
    private const int MaxValue = 99999999;

    public int Generate()
    {
        // TODO: Check better genarators
        var rand = new Random();
        return rand.Next(MinValue, MaxValue);
    }
}
