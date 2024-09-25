using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Persistence.Repositories;

public class SubscriptionRepository : BaseRepository<Subscription>, ISubscriptionRepository
{
    public SubscriptionRepository(PencilDbContext dbContext) : base(dbContext)
    {
    }
    
    public override async Task<Subscription?> GetAsync(Expression<Func<Subscription, bool>> wherePredicate, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Subscription>()
            .IgnoreQueryFilters()
            .Where(wherePredicate)
            .FirstOrDefaultAsync(cancellationToken);
    }
}