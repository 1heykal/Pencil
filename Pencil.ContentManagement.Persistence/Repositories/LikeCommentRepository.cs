using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Persistence.Repositories;

public class LikeCommentRepository : BaseRepository<Like<Comment>>, ILikeCommentRepository
{
    public LikeCommentRepository(PencilDbContext dbContext) : base(dbContext)
    {
    }
    
    public async Task<Like<Comment>> AddAsync(Guid commentId, Guid userId, CancellationToken cancellationToken = default)
    {
        var entity = new Like<Comment> { ItemId = commentId, UserId = userId };
        return await base.AddAsync(entity, cancellationToken);
    }
}