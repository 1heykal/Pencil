using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pencil.ContentManagement.Application.Features.Account.Commands.UpdateProfile;
using Pencil.ContentManagement.Application.Features.Account.Queries.GetProfile;
using Pencil.ContentManagement.Application.Features.Account.Queries.GetProfile.LoggedUser;
using Pencil.ContentManagement.Application.Features.Blogs.Queries.GetBlog;
using Pencil.ContentManagement.Application.Features.Posts.Commands.CreatePost;
using Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPut("Profile")][Authorize]
    public async Task<ActionResult> UpdateProfile(UpdateProfileCommand command)
    {
        var response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response.StatusCode is StatusCodes.Status204NoContent? null : response);
    }
    
    [HttpGet("Profile")][Authorize]
    public async Task<ActionResult<BaseResponse<ProfileDetailsDto>?>> GetProfileDetails()
    {
        var response = await _mediator.Send(new GetLoggedUserProfileDetailsQuery());
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpGet("{username}")]
    public async Task<ActionResult<BaseResponse<ProfileDetailsDto>?>> GetProfileDetails(string username)
    {
        var response = await _mediator.Send(new GetProfileDetailsQuery{ Username = username });
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpGet("Posts")][Authorize]
    public async Task<ActionResult<BaseResponse<List<PostsDto>>?>> GetPosts()
    {
        var response = await _mediator.Send(new GetPostsByLoggedUserIdQuery());
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpGet("LikedPosts")][Authorize]
    public async Task<ActionResult<BaseResponse<List<PostsDto>>>> GetLikedPosts()
    {
        var response = await _mediator.Send(new GetLikedPostsByLoggedUserIdQuery());
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpGet("Blogs")][Authorize]
    public async Task<ActionResult<BaseResponse<List<BlogInfoDto>>?>> GetBlogs()
    {
        var response = await _mediator.Send(new GetBlogsByUserIdQuery());
        return StatusCode(response.StatusCode, response);
    }
}