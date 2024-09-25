using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Persistence.Repositories;

public class LikePostRepository : BaseRepository<Like<Post>>, ILikePostRepository
{
    public LikePostRepository(PencilDbContext dbContext) : base(dbContext)
    {
    }
    
    public async Task<Like<Post>> AddAsync(Guid postId, Guid userId, CancellationToken cancellationToken = default)
    {
        var entity = new Like<Post> { ItemId = postId, UserId = userId };
        return await base.AddAsync(entity, cancellationToken);
    }
}