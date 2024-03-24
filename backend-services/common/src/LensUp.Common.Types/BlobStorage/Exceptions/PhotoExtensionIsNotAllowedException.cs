namespace LensUp.Common.Types.BlobStorage.Exceptions;

public class PhotoExtensionIsNotAllowedException : Exception
{
    public PhotoExtensionIsNotAllowedException(string extension) : base($"File extension {extension} is not allowed.")
    {
    }
}
