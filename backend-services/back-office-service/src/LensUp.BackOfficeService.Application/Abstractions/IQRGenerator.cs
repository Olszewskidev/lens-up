namespace LensUp.BackOfficeService.Application.Abstractions;

public interface IQRGenerator
{
    MemoryStream Generate(Uri uri);
}
