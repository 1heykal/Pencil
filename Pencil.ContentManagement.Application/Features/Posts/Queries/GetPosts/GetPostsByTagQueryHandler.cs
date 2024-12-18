using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Resources;
using Pencil.ContentManagement.Application.Responses;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;

public class GetPostsByTagQueryHandler : IRequestHandler<GetPostsByTagQuery, BaseResponse<IReadOnlyList<PostsDto>>>
{
    private readonly IPostRepository _postRepository;
    private readonly IAsyncRepository<Tag> _tagRepository;


    public GetPostsByTagQueryHandler(IPostRepository postRepository,IAsyncRepository<Tag> tagRepository)
    {
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
    }

    public async Task<BaseResponse<IReadOnlyList<PostsDto>>> Handle(GetPostsByTagQuery request, CancellationToken cancellationToken)
    {
        var userExists = await _tagRepository.ExistsAsync(t => t.Name.Equals(request.Name), cancellationToken);
        
        if (!userExists)
            return new(false, Shared.UserNotFound, StatusCodes.Status404NotFound);

        var posts = await _postRepository.GetAllPostsDtoAsync(p => p.Tags.Any(t => t.Name.Equals(request.Name)), cancellationToken);
        return new(posts);
    }
}