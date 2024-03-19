namespace LensUp.Common.AzureBlobStorage.Extensions;

public class PhotoExtenionIsNotAllowedException : Exception
{
    public PhotoExtenionIsNotAllowedException(string extension) : base($"File extension {extension} is not allowed.")
    {       
    }
}
