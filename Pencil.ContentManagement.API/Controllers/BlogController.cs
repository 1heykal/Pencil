using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pencil.ContentManagement.Application.Features.Blogs.Commands;
using Pencil.ContentManagement.Application.Features.Blogs.Commands.CreateBlog;
using Pencil.ContentManagement.Application.Features.Blogs.Commands.DeleteBlog;
using Pencil.ContentManagement.Application.Features.Blogs.Queries.GetBlog;
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

    [HttpPost]
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
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<BaseResponse<string>>> DeleteBlog(Guid id)
    {
        var response = await _mediator.Send(new DeleteBlogCommand{ Id = id });
        return StatusCode(response.StatusCode,
            response.StatusCode is StatusCodes.Status204NoContent ? null : response);
    }

    
}