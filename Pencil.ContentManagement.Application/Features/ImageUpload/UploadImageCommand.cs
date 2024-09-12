using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.ImageUpload;

public class UploadImageCommand : IRequest<BaseResponse<List<UploadedImageDto>>>
{
    public List<IFormFile> Files { get; set; }
}