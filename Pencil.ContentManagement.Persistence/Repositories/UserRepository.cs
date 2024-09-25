using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Account.Queries.GetProfile;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Persistence.Repositories;

public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
{
    private readonly IMapper _mapper;
    public UserRepository(PencilDbContext dbContext, IMapper mapper) : base(dbContext)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<ApplicationUser?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<ApplicationUser>()
            .FirstOrDefaultAsync(a => a.Email.Equals(email), cancellationToken);
    }

    public async Task<ProfileDetailsDto?> GetProfileDetailsAsync(Expression<Func<ApplicationUser, bool>> wherePredicate, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<ApplicationUser>()
            .Where(wherePredicate)
            .Include(u => u.Following)
            .Include(u => u.Followers)
            .Select(u => _mapper.Map<ProfileDetailsDto>(u))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbContext.Set<ApplicationUser>().AnyAsync(u => u.Id.Equals(id));
    }
}