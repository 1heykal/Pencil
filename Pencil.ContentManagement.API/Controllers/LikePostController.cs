using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pencil.ContentManagement.Application.Features.Likes;
using Pencil.ContentManagement.Application.Features.Likes.Commands;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class LikePostController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public LikePostController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost("Like")]
    public async Task<ActionResult<BaseResponse<string>>> LikeAsync(LikePostCommand command)
    {
        var response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost("Unlike")]
    public async Task<ActionResult<BaseResponse<string>>> UnlikeAsync(UnlikePostCommand command)
    {
        var response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }
}