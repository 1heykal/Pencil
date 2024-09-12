using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;

public class GetPostsByUserIdQueryHandler : IRequestHandler<GetPostsByUserIdQuery, BaseResponse<List<PostsDto>>>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;


    public GetPostsByUserIdQueryHandler(IPostRepository postRepository, IMapper mapper, IUserRepository userRepository)
    {
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<BaseResponse<List<PostsDto>>> Handle(GetPostsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<List<PostsDto>>();
        var userExists = await _userRepository.ExistsAsync(request.UserId);
        if (!userExists)
        {
            response.ValidationErrors = [$"There is no user with the specified Id: {request.UserId}"];
            response.StatusCode = StatusCodes.Status404NotFound;
            response.Success = false;
            return response;
        } 
            
        var posts = (await _postRepository.GetPostsByUserId(request.UserId, cancellationToken)).OrderByDescending(p => p.PublishedOn);
        response.Data = _mapper.Map<List<PostsDto>>(posts);
        return response;
    }
}