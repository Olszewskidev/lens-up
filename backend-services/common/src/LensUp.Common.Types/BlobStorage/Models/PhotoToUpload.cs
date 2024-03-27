using LensUp.Common.Types.BlobStorage.Constants;
using LensUp.Common.Types.BlobStorage.Exceptions;

namespace LensUp.Common.Types.BlobStorage.Models;

public sealed class PhotoToUpload
{
    public string FileName { get; init; }

    public Stream? StreamContent { get; init; }

    public byte[]? ByteArrayContent { get; init; }

    private PhotoToUpload(string fileName, Stream content)
    {
        this.FileName = fileName;
        this.StreamContent = content;
    }

    private PhotoToUpload(string fileName, byte[] content)
    {
        this.FileName = fileName;
        this.ByteArrayContent = content;
    }

    public static PhotoToUpload Create(string fileName, Stream content)
    {
        var photoExtension = Path.GetExtension(fileName).ToLower();
        if (!PhotoFileExtensions.AllowedToUpload.Contains(photoExtension))
        {
            throw new PhotoExtensionIsNotAllowedException(photoExtension);
        }

        if (content == null) 
        { 
            throw new ArgumentNullException(nameof(content));
        }

        return new PhotoToUpload(fileName, content);
    }

    public static PhotoToUpload Create(string fileName, byte[] content)
    {
        var photoExtension = Path.GetExtension(fileName).ToLower();
        if (!PhotoFileExtensions.AllowedToUpload.Contains(photoExtension))
        {
            throw new PhotoExtensionIsNotAllowedException(photoExtension);
        }

        if (content == null)
        {
            throw new ArgumentNullException(nameof(content));
        }

        return new PhotoToUpload(fileName, content);
    }
}
