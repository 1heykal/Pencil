using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Posts.Commands.CreatePost;
using Pencil.ContentManagement.Application.Responses;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Features.Posts.Queries.GetPost;

public class GetPostByUrlQueryHandler : IRequestHandler<GetPostByUrlQuery, BaseResponse<CreatePostDto>>
{
    private readonly IAsyncRepository<Post> _postRepository;
    private readonly IMapper _mapper;

    public GetPostByUrlQueryHandler(IAsyncRepository<Post> postRepository, IMapper mapper)
    {
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<BaseResponse<CreatePostDto>> Handle(GetPostByUrlQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<CreatePostDto>();

        var post = await _postRepository.GetAsync(post => post.Url == request.Url, cancellationToken);

        if (post is null)
        {
            response.Success = false;
            response.StatusCode = StatusCodes.Status404NotFound;
            return response;
        }

        var data = _mapper.Map<CreatePostDto>(post);
        response.Data = data;

        return response;
    }
}