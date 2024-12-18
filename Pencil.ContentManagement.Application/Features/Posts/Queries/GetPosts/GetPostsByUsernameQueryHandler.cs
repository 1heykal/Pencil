using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Resources;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;

public class GetPostsByUsernameQueryHandler : IRequestHandler<GetPostsByUsernameQuery, BaseResponse<IReadOnlyList<PostsDto>>>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;


    public GetPostsByUsernameQueryHandler(IPostRepository postRepository, IUserRepository userRepository)
    {
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<BaseResponse<IReadOnlyList<PostsDto>>> Handle(GetPostsByUsernameQuery request, CancellationToken cancellationToken)
    {
        var userExists = await _userRepository.ExistsAsync(u => u.Username.Equals(request.Username), cancellationToken);
        
        if (!userExists)
            return new(false, Shared.UserNotFound, StatusCodes.Status404NotFound);

        var posts = await _postRepository.GetAllPostsDtoAsync(p=> p.Author.Username.Equals(request.Username), cancellationToken);
        return new (posts);
    }
}