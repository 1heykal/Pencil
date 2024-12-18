using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Posts.Commands.CreatePost;
using Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;
using Pencil.ContentManagement.Application.Responses;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Features.Posts.Queries.GetPost;

public class GetPostByUrlQueryHandler : IRequestHandler<GetPostByUrlQuery, BaseResponse<PostsDto>>
{
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;

    public GetPostByUrlQueryHandler(IPostRepository postRepository, IMapper mapper)
    {
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<BaseResponse<PostsDto>> Handle(GetPostByUrlQuery request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetPostsDtoAsync(post => post.Url.Equals(request.Url), cancellationToken);

        if (post is null)
            return new ([$"Can't find post with the specified url: {request.Url}"], StatusCodes.Status404NotFound);
        
        return new(_mapper.Map<PostsDto>(post));
    }
}