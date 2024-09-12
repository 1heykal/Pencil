using Microsoft.EntityFrameworkCore;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Persistence.Repositories;

public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
{
    public UserRepository(PencilDbContext dbContext) : base(dbContext)
    {
    }
    
    public async Task<ApplicationUser?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<ApplicationUser>()
            .FirstOrDefaultAsync(a => a.Email.Equals(email), cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbContext.Set<ApplicationUser>().AnyAsync(u => u.Id.Equals(id));
    }
}