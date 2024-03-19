using LensUp.Common.AzureBlobStorage.Constants;
using LensUp.Common.AzureBlobStorage.Extensions;

namespace LensUp.Common.AzureBlobStorage.Models;

public sealed class PhotoToUpload
{
    public string Id { get; init; }

    public string FileName { get; init; }

    public Stream Content { get; init; }

    private PhotoToUpload(string id, string fileName, Stream content)
    {
        this.Id = id;
        this.FileName = fileName;
        this.Content = content;
    }

    public static PhotoToUpload Create(string id, string fileName, Stream content)
    {
        var photoExtension = Path.GetExtension(fileName).ToLower();
        if (!PhotoFileExtensions.AllowedToUpload.Contains(photoExtension))
        {
            throw new PhotoExtenionIsNotAllowedException(photoExtension);
        }
        return new PhotoToUpload(id, fileName, content);
    }
}
