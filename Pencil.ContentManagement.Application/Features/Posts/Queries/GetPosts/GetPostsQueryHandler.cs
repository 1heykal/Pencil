using AutoMapper;
using MediatR;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Responses;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;

public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, BaseResponse<List<PostsDto>>>
{
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;


    public GetPostsQueryHandler(IPostRepository postRepository, IMapper mapper)
    {
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<BaseResponse<List<PostsDto>>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = (await _postRepository.GetPostsWithUserInfo(cancellationToken)).OrderByDescending(p => p.PublishedOn);
        var response = new BaseResponse<List<PostsDto>>(_mapper.Map<List<PostsDto>>(posts));
        return response;
    }
}