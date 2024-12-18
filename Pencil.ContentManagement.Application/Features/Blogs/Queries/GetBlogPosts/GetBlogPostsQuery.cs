using MediatR;
using Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Blogs.Queries.GetBlogPosts;

public class GetBlogPostsQuery : IRequest<BaseResponse<IReadOnlyList<PostsDto>>>
{
    public Guid BlogId { get; set; }
}