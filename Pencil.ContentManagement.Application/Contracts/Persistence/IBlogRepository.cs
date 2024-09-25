using System.Linq.Expressions;
using Pencil.ContentManagement.Application.Features.Blogs.Queries.GetBlog;
using Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Contracts.Persistence;

public interface IBlogRepository : IAsyncRepository<Blog>
{
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<BlogInfoDto?> GetBlogInfo(Guid id, CancellationToken cancellationToken = default);
    
    Task<BlogInfoDto?> GetBlogInfo(Expression<Func<Blog, bool>> wherePredicate, CancellationToken cancellationToken = default);
    
    Task SoftDelete(Blog entity, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyList<BlogPostsDto>> GetPostsDtoAsync(Expression<Func<Post, bool>> wherePredicate, CancellationToken cancellationToken = default);
    
}