using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pencil.ContentManagement.Application.Features.Posts.Commands.CreatePost;
using Pencil.ContentManagement.Application.Features.Posts.Commands.DeletePost;
using Pencil.ContentManagement.Application.Features.Posts.Commands.UpdatePost;
using Pencil.ContentManagement.Application.Features.Posts.Queries.GetFeedPosts;
using Pencil.ContentManagement.Application.Features.Posts.Queries.GetPost;
using Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<List<PostsDto>>>> GetAllPosts()
        {
            var response = await _mediator.Send(new GetPostsQuery());
            return StatusCode(response.StatusCode, response);
        }
        
        [HttpGet("Feed")][Authorize]
        public async Task<ActionResult<BaseResponse<List<PostsDto>>>> GetFeedPosts()
        {
            var timer = new Stopwatch();
            timer.Start();
            
            var response = await _mediator.Send(new GetFeedPostsQuery());
            
            timer.Stop();
            var time = timer.ElapsedMilliseconds;
            
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost][Authorize]
        public async Task<ActionResult<BaseResponse<CreatePostDto>>> AddPost(CreatePostCommand command)
        {
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{url}", Name ="GetPostByUrl")]
        public async Task<ActionResult<BaseResponse<PostsDto>>> GetPostByUrl(string url)
        {
            var response = await _mediator.Send(new GetPostByUrlQuery { Url = url });
            return StatusCode(response.StatusCode, response);
        }
        
        [HttpGet("pid/{id}", Name ="GetPostById")]
        public async Task<ActionResult<BaseResponse<PostsDto>>> GetPostById(Guid id)
        {
            var response = await _mediator.Send(new GetPostByIdQuery { Id = id });
            return StatusCode(response.StatusCode, response);
        }
        
        [HttpGet("username/{username}", Name ="GetPostsByUsername")]
        public async Task<ActionResult<BaseResponse<List<PostsDto>>>> GetPostsByUsername(string username)
        {
            var response = await _mediator.Send(new GetPostsByUsernameQuery { Username = username });
            return StatusCode(response.StatusCode, response);
        }
        
        [HttpGet("u/{uid}", Name ="GetPostsByUserId")]
        public async Task<ActionResult<BaseResponse<List<PostsDto>>?>> GetPostsByUserId(Guid uid)
        {
            var response = await _mediator.Send(new GetPostsByUserIdQuery { UserId = uid });
            return StatusCode(response.StatusCode, response);
        }
        
        [HttpGet("t/{name}", Name ="GetPostsByTag")]
        public async Task<ActionResult<IReadOnlyList<PostsDto>>> GetPostsByUserId(string name)
        {
            var response = await _mediator.Send(new GetPostsByTagQuery { Name = name });
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")][Authorize]
        public async Task<ActionResult<BaseResponse<string>>> DeletePost(Guid id)
        {
            var response = await _mediator.Send(new DeletePostCommand { Id = id });
            return StatusCode(response.StatusCode,
                response.StatusCode is StatusCodes.Status204NoContent ? null : response);
        }
        
        [HttpPut][Authorize]
        public async Task<ActionResult<BaseResponse<string>>> UpdatePost(UpdatePostCommand command)
        {
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode,
                response.StatusCode is StatusCodes.Status204NoContent ? null : response);
        }
        
    }
}
