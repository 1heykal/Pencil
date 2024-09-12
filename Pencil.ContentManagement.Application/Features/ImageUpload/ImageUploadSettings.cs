namespace Pencil.ContentManagement.API.Middlewares;

public class ImageUploadSettings
{
    public string[] SupportedTypes { get; set; }

    public long MaxFileSize { get; set; }
}