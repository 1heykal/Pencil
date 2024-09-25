using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pencil.ContentManagement.Application.Features.Likes;
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

    [HttpPost]
    public async Task<ActionResult<BaseResponse<string>>> LikeAsync(Guid postId)
    {
        var response = await _mediator.Send(new LikePostCommand { PostId = postId });
        return StatusCode(response.StatusCode, response);
    }
}