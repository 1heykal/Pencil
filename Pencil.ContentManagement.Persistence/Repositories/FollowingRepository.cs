using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Persistence.Repositories;

public class FollowingRepository : BaseRepository<Following>, IFollowingRepository
{
    public FollowingRepository(PencilDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<Following?> GetAsync(Expression<Func<Following, bool>> wherePredicate, CancellationToken cancellationToken = default)
    {
        
        return await _dbContext.Set<Following>()
            .IgnoreQueryFilters()
            .Where(wherePredicate)
            .FirstOrDefaultAsync(cancellationToken);
    }
}