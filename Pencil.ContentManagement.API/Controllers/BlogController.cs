using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pencil.ContentManagement.Application.Features.Blogs.Commands.CreateBlog;
using Pencil.ContentManagement.Application.Features.Blogs.Commands.DeleteBlog;
using Pencil.ContentManagement.Application.Features.Blogs.Commands.UpdateBlog;
using Pencil.ContentManagement.Application.Features.Blogs.Queries.GetBlog;
using Pencil.ContentManagement.Application.Features.Blogs.Queries.GetBlogPosts;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly IMediator _mediator;

    public BlogController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost][Authorize]
    public async Task<ActionResult<BaseResponse<CreatedBlogDto>>> CreateBlog(CreateBlogCommand command)
    {
        var response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("info/{id}")]
    public async Task<ActionResult<BaseResponse<BlogInfoDto>>> GetBlogInfo(Guid id)
    {
        var response = await _mediator.Send(new GetBlogInfoQuery { Id = id });
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpGet("info/username/{username}")]
    public async Task<ActionResult<BaseResponse<BlogInfoDto>>> GetBlogInfo(string username)
    {
        var response = await _mediator.Send(new GetBlogByUsernameQuery { Username = username });
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpDelete("{id}")][Authorize]
    public async Task<ActionResult<BaseResponse<string>>> DeleteBlog(Guid id)
    {
        var response = await _mediator.Send(new DeleteBlogCommand{ Id = id });
        return StatusCode(response.StatusCode,
            response.StatusCode is StatusCodes.Status204NoContent ? null : response);
    }
    
    [HttpPut("info")][Authorize]
    public async Task<ActionResult<BaseResponse<string>>> UpdateBlogInfo(UpdateBlogInfoCommand command)
    {
        var response = await _mediator.Send(command);
        return StatusCode(response.StatusCode,
            response.StatusCode is StatusCodes.Status204NoContent ? null : response);
    }
    
    [HttpGet("{id}/posts")]
    public async Task<ActionResult<BaseResponse<BlogInfoDto>>> GetBlogPosts(Guid id)
    {
        var response = await _mediator.Send(new GetBlogPostsQuery {  BlogId = id });
        return StatusCode(response.StatusCode, response);
    }
    
  
    
}