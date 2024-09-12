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

    public async Task<List<PostsDto>> GetPostsWithUserInfo(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Post>().Include(p => p.Author)
            .Include(p => p.Blog)
            .Select(p => _mapper.Map<PostsDto>(p)).ToListAsync(cancellationToken);
    }

    public async Task<List<PostsDto>> GetPostsByUserId(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Post>().Where(p => p.AuthorId.Equals(id)).Include(p => p.Author)
            .Select(p => _mapper.Map<PostsDto>(p)).ToListAsync(cancellationToken);
    }

    public async Task SoftDelete(Post entity, CancellationToken cancellationToken = default)
    {
        entity.SoftDeleted = true;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}