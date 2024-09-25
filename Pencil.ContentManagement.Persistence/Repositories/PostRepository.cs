using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Persistence.Repositories;

public class PostRepository : BaseRepository<Post>, IPostRepository
{
    private readonly IMapper _mapper;
    
    public PostRepository(PencilDbContext dbContext, IMapper mapper) : base(dbContext)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<IReadOnlyList<PostsDto>> GetPostsDtoAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Post>()
            .OrderByDescending(p => p.PublishedOn)
            .Select(p => _mapper.Map<PostsDto>(p))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<PostsDto>> GetPostsDtoAsync(Expression<Func<Post, bool>> wherePredicate, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Post>().Where(wherePredicate)
            .OrderByDescending(p => p.PublishedOn)
            .Select(p => _mapper.Map<PostsDto>(p))
            .ToListAsync(cancellationToken);
    }

    public async Task<List<PostsDto>> GetFeedPostsByUserId(Guid id, CancellationToken cancellationToken = default)
    {

        var authorsIds = _dbContext.Set<Following>()
            .Where(f => f.FollowerId.Equals(id))
            .Select(f => f.FollowedId);

        var blogsIds =  _dbContext.Set<Subscription>()
            .Where(s => s.UserId.Equals(id))
            .Select(f => f.BlogId);
        
        return await _dbContext.Set<Post>()
            .Where(p =>  authorsIds.Any(u => u.Equals(p.AuthorId)) || blogsIds.Any(u => u.Equals(p.BlogId)))
            .Include(p => p.Author)
            .Include(p => p.Blog)
            .Include(p => p.Comments)
            .Include(p => p.Likes)
            .OrderByDescending(p => p.PublishedOn)
            .Select(p => _mapper.Map<PostsDto>(p))
            .ToListAsync(cancellationToken);
    }

    public async Task<List<PostsDto>> GetPostsWithUserInfo(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Post>()
            .Include(p => p.Author)
            .Include(p => p.Blog)
            .OrderByDescending(p => p.PublishedOn)
            .Select(p => _mapper.Map<PostsDto>(p))
            .ToListAsync(cancellationToken);
    }

    public async Task<List<PostsDto>> GetPostsByUserId(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Post>()
            .Where(p => p.AuthorId.Equals(id))
            .Include(p => p.Author)
            .OrderByDescending(p => p.PublishedOn)
            .Select(p => _mapper.Map<PostsDto>(p))
            .ToListAsync(cancellationToken);
    }

    public async Task SoftDelete(Post entity, CancellationToken cancellationToken = default)
    {
        entity.SoftDeleted = true;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}