using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Persistence.Repositories;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    public CommentRepository(PencilDbContext dbContext) : base(dbContext)
    {
    }

    public async Task SoftDelete(Comment entity, CancellationToken cancellationToken = default)
    {
        entity.SoftDeleted = true;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}