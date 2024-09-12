using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pencil.ContentManagement.Application.Features.ImageUpload;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImageUploadController : ControllerBase
{
    private readonly IMediator _mediator;

    public ImageUploadController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<List<UploadedImageDto>>>> UploadImage(UploadImageCommand command)
    {
        var response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }
    
}