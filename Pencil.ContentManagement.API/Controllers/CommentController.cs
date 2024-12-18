using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pencil.ContentManagement.Application.Features.Comments.Commands.CreateComment;
using Pencil.ContentManagement.Application.Features.Comments.Commands.DeleteComment;
using Pencil.ContentManagement.Application.Features.Comments.Commands.UpdateComment;
using Pencil.ContentManagement.Application.Features.Comments.Queries.GetComments;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommentController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    
    [HttpGet("pid/{pid}", Name ="GetCommentsByPostId")]
    public async Task<ActionResult<BaseResponse<CommentsDto>>> GetPostsByUserId(Guid pid)
    {
        var response = await _mediator.Send(new GetCommentsByPostIdQuery { PostId = pid });
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost][Authorize]
    public async Task<ActionResult<BaseResponse<CreateCommentDto>>> AddComment(CreateCommentCommand command)
    {
        var response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpDelete("{id}")][Authorize]
    public async Task<ActionResult<BaseResponse<string>>> DeleteComment(Guid id)
    {
        var response = await _mediator.Send(new DeleteCommentCommand { Id = id });
        return StatusCode(response.StatusCode,
            response.StatusCode is StatusCodes.Status204NoContent ? null : response);
    }
        
    [HttpPut][Authorize]
    public async Task<ActionResult<BaseResponse<string>>> UpdateComment(UpdateCommentCommand command)
    {
        var response = await _mediator.Send(command);
        return StatusCode(response.StatusCode,
            response.StatusCode is StatusCodes.Status204NoContent ? null : response);
    }
}