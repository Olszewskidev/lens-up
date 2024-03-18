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
        // TODO: validation
        return new PhotoToUpload(id, fileName, content);
    }
}
