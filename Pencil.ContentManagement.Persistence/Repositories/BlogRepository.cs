using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Auth;
using Pencil.ContentManagement.Application.Features.Blogs.Queries.GetBlog;
using Pencil.ContentManagement.Application.Features.Blogs.Queries.GetBlogPosts;
using Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Persistence.Repositories;
public class BlogRepository : BaseRepository<Blog>, IBlogRepository
{
    private readonly IMapper _mapper;
    private readonly  ISubscriptionRepository _subscriptionRepository;
    private readonly IHttpContextAccessor _httpContextAccessor; 
    
    public BlogRepository(PencilDbContext dbContext, IMapper mapper, ISubscriptionRepository subscriptionRepository, IHttpContextAccessor httpContextAccessor) : base(dbContext)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _subscriptionRepository = subscriptionRepository ?? throw new ArgumentNullException(nameof(subscriptionRepository));
    }
    public async Task<BlogInfoDto?> GetBlogInfo(Expression<Func<Blog, bool>> wherePredicate, CancellationToken cancellationToken = default)
    {
        var blogInfoDto = await _dbContext.Set<Blog>()
            .Where(wherePredicate)
            .Include(b => b.Posts)
            .Include(b => b.Subscriptions)
            .Include(b => b.Author)
            .Select(b => _mapper.Map<BlogInfoDto>(b))
            .FirstOrDefaultAsync(cancellationToken);
        
        if(blogInfoDto is not null)
            blogInfoDto.IsCurrentUserSubscribed = await _subscriptionRepository.ExistsAsync(s => s.UserId.Equals(AuthHelper.GetUserId(_httpContextAccessor).UserId) && s.BlogId.Equals(blogInfoDto.Id), cancellationToken);

        return blogInfoDto;
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Blog>().AnyAsync(b => b.Id.Equals(id), cancellationToken);
    }
    
    public async Task<BlogInfoDto?> GetBlogInfo(Guid id, CancellationToken cancellationToken = default)
    {
        var blogInfoDto = await _dbContext.Set<Blog>()
            .Where(b => b.Id.Equals(id))
            .Include(b => b.Posts)
            .Include(b => b.Subscriptions)
            .Include(b => b.Author)
            .Select(b => _mapper.Map<BlogInfoDto>(b))
            .FirstOrDefaultAsync(cancellationToken);
        
        if(blogInfoDto is not null)
            blogInfoDto.IsCurrentUserSubscribed = await _subscriptionRepository.ExistsAsync(s => s.UserId.Equals(AuthHelper.GetUserId(_httpContextAccessor).UserId) && s.BlogId.Equals(id), cancellationToken);

        return blogInfoDto;
    }
    
    public async Task<List<BlogInfoDto>> GetBlogsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Blog>()
            .Where(b => b.AuthorId.Equals(userId))
            .Include(b => b.Posts)
            .Include(b => b.Subscriptions)
            .Include(b => b.Author)
            .Select(b => _mapper.Map<BlogInfoDto>(b))
            .ToListAsync(cancellationToken);
    }
    
    public async Task SoftDelete(Blog entity, CancellationToken cancellationToken = default)
    {
        entity.SoftDeleted = true;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<PostsDto>> GetPostsDtoAsync(Expression<Func<Post, bool>> wherePredicate, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Post>()
            .Where(wherePredicate)
            .Include(p => p.Author)
            .OrderByDescending(p => p.PublishedOn)
            .Select(p => _mapper.Map<PostsDto>(p))
            .ToListAsync(cancellationToken);
    }
}