using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Auth;
using Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Blogs.Queries.GetBlogPosts;

public class GetBlogPostsQueryHandler : IRequestHandler<GetBlogPostsQuery, BaseResponse<IReadOnlyList<PostsDto>>>
{
    private readonly IBlogRepository _blogRepository;
    private readonly IPostRepository _postRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetBlogPostsQueryHandler(IBlogRepository blogRepository, IPostRepository postRepository, IHttpContextAccessor httpContextAccessor)
    {
        _blogRepository = blogRepository ?? throw new ArgumentNullException(nameof(blogRepository));
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }
   

    public async Task<BaseResponse<IReadOnlyList<PostsDto>>> Handle(GetBlogPostsQuery request, CancellationToken cancellationToken)
    {
        var userId = AuthHelper.GetUserId(_httpContextAccessor).UserId;
        
        var blogExists = await _blogRepository.ExistsAsync(request.BlogId, cancellationToken);
        
        if (!blogExists)
            return new ([$"There is no Blog with the specified Id: {request.BlogId}"],
                StatusCodes.Status404NotFound);

        IReadOnlyList<PostsDto> posts;
        if (userId != Guid.Empty)
        {
            posts =  await _postRepository.GetAllPostsDtoAsync(post => post.BlogId.Equals(request.BlogId) , cancellationToken); 
        }
        else
        {
            posts = await _postRepository.GetPostsByUserId(userId, post => post.BlogId.Equals(request.BlogId), cancellationToken);
        }
        
        return new (posts);
    }
}