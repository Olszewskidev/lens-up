using LensUp.Common.Types.BlobStorage.Constants;
using LensUp.Common.Types.BlobStorage.Extensions;

namespace LensUp.Common.Types.BlobStorage.Models;

public sealed class PhotoToUpload
{
    public string FileName { get; init; }

    public Stream Content { get; init; }

    private PhotoToUpload(string fileName, Stream content)
    {
        this.FileName = fileName;
        this.Content = content;
    }

    public static PhotoToUpload Create(string fileName, Stream content)
    {
        var photoExtension = Path.GetExtension(fileName).ToLower();
        if (!PhotoFileExtensions.AllowedToUpload.Contains(photoExtension))
        {
            throw new PhotoExtenionIsNotAllowedException(photoExtension);
        }

        return new PhotoToUpload(fileName, content);
    }
}
