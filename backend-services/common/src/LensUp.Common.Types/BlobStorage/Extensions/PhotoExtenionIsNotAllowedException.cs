namespace LensUp.Common.Types.BlobStorage.Extensions;

public class PhotoExtenionIsNotAllowedException : Exception
{
    public PhotoExtenionIsNotAllowedException(string extension) : base($"File extension {extension} is not allowed.")
    {
    }
}
