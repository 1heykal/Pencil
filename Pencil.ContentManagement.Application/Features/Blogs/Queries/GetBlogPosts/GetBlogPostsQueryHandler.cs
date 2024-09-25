using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Blogs.Queries.GetBlogPosts;

public class GetBlogPostsQueryHandler : IRequestHandler<GetBlogPostsQuery, BaseResponse<IReadOnlyList<BlogPostsDto>>>
{
    private readonly IBlogRepository _blogRepository;

    public GetBlogPostsQueryHandler(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository ?? throw new ArgumentNullException(nameof(blogRepository));
    }

    public async Task<BaseResponse<IReadOnlyList<BlogPostsDto>>> Handle(GetBlogPostsQuery request, CancellationToken cancellationToken)
    {
        var blogExists = await _blogRepository.ExistsAsync(request.BlogId);
        
        if (!blogExists)
            return new ([$"There is no Blog with the specified Id: {request.BlogId}"],
                StatusCodes.Status404NotFound);
            
        var posts = await _blogRepository.GetPostsDtoAsync(post => post.BlogId.Equals(request.BlogId), cancellationToken);
        return new (posts);
    }
}