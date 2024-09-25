using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Auth;
using Pencil.ContentManagement.Application.Features.Posts.Commands.CreatePost;
using Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Posts.Queries.GetFeedPosts;

public class GetFeedPostsQueryHandler : IRequestHandler<GetFeedPostsQuery, BaseResponse<List<PostsDto>>>
{
    private readonly IPostRepository _postRepository;
    private IHttpContextAccessor _httpContextAccessor;
    private IUserRepository _userRepository;

    public GetFeedPostsQueryHandler(IPostRepository postRepository, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
    {
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<BaseResponse<List<PostsDto>>> Handle(GetFeedPostsQuery request, CancellationToken cancellationToken)
    {
        var userId = AuthHelper.GetUserId(_httpContextAccessor).UserId;
        
        if (!await _userRepository.ExistsAsync(userId))
            return new UnauthorizedResponse<List<PostsDto>>();

        var posts = await _postRepository.GetFeedPostsByUserId(userId, cancellationToken);
        return new(posts);
    }
}