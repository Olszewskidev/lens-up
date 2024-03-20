using LensUp.BackOfficeService.Application.Abstractions;
using QRCoder;

namespace LensUp.BackOfficeService.Infrastructure.Generators;

public sealed class QRGenerator : IQRGenerator
{
    public MemoryStream Generate(Uri uri)
    {
        string urlToEncode = uri.AbsoluteUri;

        var qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(urlToEncode, QRCodeGenerator.ECCLevel.Q);
        var qrCode = new QRCode(qrCodeData);
        var qrCodeImage = qrCode.GetGraphic(20);

        var stream = new MemoryStream();
        qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
        stream.Seek(0, SeekOrigin.Begin);

        return stream;
    }
}
