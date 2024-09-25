using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pencil.ContentManagement.Application.Features.Likes;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class LikeCommentController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public LikeCommentController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<string>>> LikeAsync(Guid commentId)
    {
        var response = await _mediator.Send(new LikeCommentCommand { CommentId = commentId });
        return StatusCode(response.StatusCode, response);
    }
}