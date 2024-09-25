using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Blogs.Queries.GetBlog;
using Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Persistence.Repositories;
public class BlogRepository : BaseRepository<Blog>, IBlogRepository
{
    private readonly IMapper _mapper;
    
    public BlogRepository(PencilDbContext dbContext, IMapper mapper) : base(dbContext)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    public async Task<BlogInfoDto?> GetBlogInfo(Expression<Func<Blog, bool>> wherePredicate, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Blog>()
            .Where(wherePredicate)
            .Include(b => b.Posts)
            .Include(b => b.Subscriptions)
            .Include(b => b.Author)
            .Select(b => _mapper.Map<BlogInfoDto>(b))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Blog>().AnyAsync(b => b.Id.Equals(id), cancellationToken);
    }
    
    public async Task<BlogInfoDto?> GetBlogInfo(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Blog>()
            .Where(b => b.Id.Equals(id))
            .Include(b => b.Posts)
            .Include(b => b.Subscriptions)
            .Include(b => b.Author)
            .Select(b => _mapper.Map<BlogInfoDto>(b))
            .FirstOrDefaultAsync(cancellationToken);
    }
    
    public async Task SoftDelete(Blog entity, CancellationToken cancellationToken = default)
    {
        entity.SoftDeleted = true;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<BlogPostsDto>> GetPostsDtoAsync(Expression<Func<Post, bool>> wherePredicate, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Post>()
            .Where(wherePredicate)
            .Include(p => p.Author)
            .OrderByDescending(p => p.PublishedOn)
            .Select(p => _mapper.Map<BlogPostsDto>(p))
            .ToListAsync(cancellationToken);
    }
}