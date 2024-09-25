using System.Security.Cryptography;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.ImageUpload;

public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, BaseResponse<List<UploadedImageDto>>>
{
    private readonly ImageUploadSettings _imageUploadSettings;

    public UploadImageCommandHandler(IOptions<ImageUploadSettings> imageUploadSettings)
    {
        _imageUploadSettings = imageUploadSettings.Value;
    }

    public async Task<BaseResponse<List<UploadedImageDto>>> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        var validationErrors = new List<string>();

        foreach (var file in request.Files)
        {
            if (!_imageUploadSettings.SupportedTypes.Contains(file.ContentType.Split("/")[1]))
            {
                validationErrors.Add($"{file.FileName}: File Type: {file.ContentType} is not supported");
                continue;
            }

            if (file.Length <= 0 || file.Length > _imageUploadSettings.MaxFileSize)
            {
                validationErrors.Add($"{file.FileName}: File size is out of range.");
            }
        }

        if (validationErrors?.Count > 0)
            return new (validationErrors);

        var images = new List<UploadedImageDto>();
        var currentDirectory = Directory.GetCurrentDirectory();
        var uploadsFolder = Path.Combine(currentDirectory, "Images");
        
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        foreach (var file in request.Files)
        {
            var uniqueFileName =
                $"{Path.GetFileNameWithoutExtension(file.FileName)}" +
                $"_{RandomNumberGenerator.GetHexString(10)}" +
                $"{Path.GetExtension(file.FileName)}";

            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }

            var image = new UploadedImageDto(uniqueFileName, Path.Combine("Images", uniqueFileName));
            images.Add(image);
        }
        
        return new (images);
    }
}