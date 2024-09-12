using Microsoft.Extensions.FileProviders;

namespace Pencil.ContentManagement.API;

public static class StaticFilesConfiguration
{
    public static void ConfigureStaticFiles(this WebApplication app)
    {
        var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");

        if (!Directory.Exists(imagesPath))
            Directory.CreateDirectory(imagesPath);

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(imagesPath),
            RequestPath = "/Images"
        });
    } 
}