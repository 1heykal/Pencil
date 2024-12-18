using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pencil.ContentManagement.Application.Features.Followings.Commands;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class FollowingController : ControllerBase
{
    private readonly IMediator _mediator;

    public FollowingController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost("Follow")]
    public async Task<ActionResult<BaseResponse<string>>> FollowUser(FollowUserCommand command)
    {
        var response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpDelete("Unfollow/{id}")]
    public async Task<ActionResult<BaseResponse<string>>> UnfollowUser(Guid id)
    {
        var response = await _mediator.Send(new UnfollowUserCommand{FollowedId = id});
        return StatusCode(response.StatusCode,
            response.StatusCode is StatusCodes.Status204NoContent ? null : response);
    }
}