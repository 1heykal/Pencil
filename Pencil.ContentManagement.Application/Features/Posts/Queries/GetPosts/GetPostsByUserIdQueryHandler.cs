using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Resources;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;

public class GetPostsByUserIdQueryHandler : IRequestHandler<GetPostsByUserIdQuery, BaseResponse<List<PostsDto>>>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;


    public GetPostsByUserIdQueryHandler(IPostRepository postRepository, IUserRepository userRepository)
    {
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<BaseResponse<List<PostsDto>>> Handle(GetPostsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var userExists = await _userRepository.ExistsAsync(request.UserId);
        
        if (!userExists)
            return new(false, Shared.UserNotFound, StatusCodes.Status404NotFound);

        var posts = await _postRepository.GetPostsByUserId(request.UserId, cancellationToken);
        return new(posts);
    }
}