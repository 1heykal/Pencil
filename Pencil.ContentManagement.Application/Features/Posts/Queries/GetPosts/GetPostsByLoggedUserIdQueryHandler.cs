using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Auth;
using Pencil.ContentManagement.Application.Resources;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;

public class GetPostsByLoggedUserIdQueryHandler : IRequestHandler<GetPostsByLoggedUserIdQuery, BaseResponse<List<PostsDto>>>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public GetPostsByLoggedUserIdQueryHandler(IPostRepository postRepository, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
    {
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<BaseResponse<List<PostsDto>>> Handle(GetPostsByLoggedUserIdQuery request, CancellationToken cancellationToken)
    {
        var userId = AuthHelper.GetUserId(_httpContextAccessor).UserId;
        
        if (!await _userRepository.ExistsAsync(userId))
            return new UnauthorizedResponse<List<PostsDto>>();
        
        var userExists = await _userRepository.ExistsAsync(userId);
        
        if (!userExists)
            return new(false, Shared.UserNotFound, StatusCodes.Status404NotFound);

        var posts = await _postRepository.GetPostsByUserId(userId, cancellationToken);
        return new(posts);
    }
}