using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Auth;
using Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Persistence.Repositories;

public class PostRepository : BaseRepository<Post>, IPostRepository
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public PostRepository(PencilDbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(dbContext)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }
    
    public async Task<IReadOnlyList<PostsDto>> GetPostsDtoAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Post>()
            .OrderByDescending(p => p.PublishedOn)
            .Select(p => _mapper.Map<PostsDto>(p))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<PostsDto>> GetAllPostsDtoAsync(Expression<Func<Post, bool>> wherePredicate, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Post>().Where(wherePredicate)
            .Include(p => p.Author)
            .Include(p => p.Likes)
            .Include(p => p.Comments)
            .OrderByDescending(p => p.PublishedOn)
            .Select(p => _mapper.Map<PostsDto>(p))
            .ToListAsync(cancellationToken);
    }
    
    public async Task<PostsDto?> GetPostsDtoAsync(Expression<Func<Post, bool>> wherePredicate, CancellationToken cancellationToken = default)
    {
        var savedPostsIds = _dbContext.Set<Box>()
            .Where(l => l.CreatorId.Equals(AuthHelper.GetUserId(_httpContextAccessor).UserId))
            .Include(b => b.Posts)
            .AsNoTracking()
            .SelectMany(b => b.Posts.Select(p => p.Id));
        
        var postDto = await _dbContext.Set<Post>().Where(wherePredicate)
            .Include(p => p.Author)
            .Include(p => p.Tags)
            .Include(p => p.Blog)
            .Include(p => p.Comments)
            .Include(p => p.Likes)
            .OrderByDescending(p => p.PublishedOn)
            .Select(p => _mapper.Map<PostsDto>(p))
            .FirstOrDefaultAsync(cancellationToken);

        if (postDto is not null)
        {
            postDto.Liked = await _dbContext.Set<Like<Post>>().AsNoTracking().AnyAsync(l => l.UserId.Equals(AuthHelper.GetUserId(_httpContextAccessor).UserId) && l.ItemId.Equals(postDto.Id), cancellationToken);
            postDto.Saved = await savedPostsIds.AnyAsync(s => s.Equals(postDto.Id));
        }
              
        return postDto;
      
    }
    

    public async Task<List<PostsDto>> GetFeedPostsByUserId(Guid id, CancellationToken cancellationToken = default)
    {
        var authorsIds = _dbContext.Set<Following>()
            .Where(f => f.FollowerId.Equals(id))
            .AsNoTracking()
            .Select(f => f.FollowedId);

        var blogsIds =  _dbContext.Set<Subscription>()
            .Where(s => s.UserId.Equals(id))
            .AsNoTracking()
            .Select(f => f.BlogId);

        var likedPostsIds = _dbContext.Set<Like<Post>>()
            .Where(l => l.UserId.Equals(id))
            .AsNoTracking()
            .Select(l => l.ItemId);
        
        var savedPostsIds = _dbContext.Set<Box>()
            .Where(l => l.CreatorId.Equals(id))
            .Include(b => b.Posts)
            .AsNoTracking()
            .SelectMany(b => b.Posts.Select(p => p.Id));
        
        
         var posts = await _dbContext.Set<Post>()
            .Where(p =>  authorsIds.Any(u => u.Equals(p.AuthorId)) || blogsIds.Any(u => u.Equals(p.BlogId) || p.AuthorId.Equals(id)))
            .Include(p => p.Author)
            .Include(p => p.Blog)
            .Include(p => p.Comments)
            .Include(p => p.Likes)
            .OrderByDescending(p => p.PublishedOn)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
         
         var postsDto = posts.Select(p =>
         {
             var postDto = _mapper.Map<PostsDto>(p);
             postDto.Liked = likedPostsIds.Any(l => l == p.Id);
             postDto.Saved = savedPostsIds.Any(s => s.Equals(p.Id));
             return postDto;
             
         }).ToList();
         
         return postsDto;
    }
    
    public async Task<List<PostsDto>> GetLikedPostsByUserId(Guid id, CancellationToken cancellationToken = default)
    {

        var postsIds = _dbContext.Set<Like<Post>>()
            .Where(f => f.UserId.Equals(id))
            .AsNoTracking()
            .Select(f => f.ItemId);
        
       var posts = await _dbContext.Set<Post>()
            .Where(p =>  postsIds.Any(i => i.Equals(p.Id)))
            .Include(p => p.Author)
            .Include(p => p.Blog)
            .Include(p => p.Comments)
            .Include(p => p.Likes)
            .OrderByDescending(p => p.PublishedOn)
            .AsNoTracking()
            .Select(p => _mapper.Map<PostsDto>(p))
            .ToListAsync(cancellationToken);
       
       var savedPostsIds = _dbContext.Set<Box>()
           .Where(l => l.CreatorId.Equals(id))
           .Include(b => b.Posts)
           .AsNoTracking()
           .SelectMany(b => b.Posts.Select(p => p.Id));
       
       return posts.Select(p =>
       {
           var postDto = _mapper.Map<PostsDto>(p);
           postDto.Liked = true;
           postDto.Saved = savedPostsIds.Any(s => s.Equals(p.Id));
           return postDto;
             
       }).ToList();
    }

    public async Task<List<PostsDto>> GetPostsWithUserInfo(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Post>()
            .Include(p => p.Author)
            .Include(p => p.Blog)
            .Include(p => p.Comments)
            .Include(p => p.Likes)
            .OrderByDescending(p => p.PublishedOn)
            .Select(p => _mapper.Map<PostsDto>(p))
            .ToListAsync(cancellationToken);
    }
    
    public async Task<List<PostsDto>> GetPostsByUserId(Guid id, Expression<Func<Post, bool>> wherePredicate, CancellationToken cancellationToken = default)
    {
        
        
        var savedPostsIds = _dbContext.Set<Box>()
            .Where(l => l.CreatorId.Equals(id))
            .Include(b => b.Posts)
            .AsNoTracking()
            .SelectMany(b => b.Posts.Select(p => p.Id));
        
        var posts = await _dbContext.Set<Post>()
            .Where(p => p.AuthorId.Equals(id))
            .Where(wherePredicate)
            .Include(p => p.Author)
            .Include(p => p.Likes)
            .Include(p => p.Comments)
            .OrderByDescending(p => p.PublishedOn)
            .Select(p => _mapper.Map<PostsDto>(p))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
         
        return posts.Select(p =>
        {
            var postDto = _mapper.Map<PostsDto>(p);
            postDto.Liked = _dbContext.Set<Like<Post>>().AsNoTracking().Any(l => l.UserId.Equals(id) && l.ItemId.Equals(p.Id));
            postDto.Saved = savedPostsIds.Any(s => s.Equals(p.Id));
            return postDto;
             
        }).ToList();
    }

    public async Task<List<PostsDto>> GetPostsByUserId(Guid id, CancellationToken cancellationToken = default)
    {
        
        var likedPostsIds = _dbContext.Set<Like<Post>>()
            .Where(l => l.UserId.Equals(id))
            .AsNoTracking()
            .Select(l => l.ItemId);
        
        var savedPostsIds = _dbContext.Set<Box>()
            .Where(l => l.CreatorId.Equals(id))
            .Include(b => b.Posts)
            .AsNoTracking()
            .SelectMany(b => b.Posts.Select(p => p.Id));
        
         var posts = await _dbContext.Set<Post>()
            .Where(p => p.AuthorId.Equals(id))
            .Include(p => p.Author)
            .Include(p => p.Likes)
            .Include(p => p.Comments)
            .OrderByDescending(p => p.PublishedOn)
            .Select(p => _mapper.Map<PostsDto>(p))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
         
         return posts.Select(p =>
         {
             var postDto = _mapper.Map<PostsDto>(p);
             postDto.Liked = likedPostsIds.Any(l => l == p.Id);
             postDto.Saved = savedPostsIds.Any(s => s.Equals(p.Id));
             return postDto;
             
         }).ToList();
    }

    public async Task SoftDelete(Post entity, CancellationToken cancellationToken = default)
    {
        entity.SoftDeleted = true;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}