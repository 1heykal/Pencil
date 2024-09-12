using Pencil.ContentManagement.Application.Features.Blogs.Queries.GetBlog;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Contracts.Persistence;

public interface IBlogRepository : IAsyncRepository<Blog>
{
    Task<bool> ExistsAsync(Guid id);
    
    public Task<BlogInfoDto?> GetBlogInfo(Guid id, CancellationToken cancellationToken = default);
    
    public Task SoftDelete(Blog entity, CancellationToken cancellationToken = default);
    
}