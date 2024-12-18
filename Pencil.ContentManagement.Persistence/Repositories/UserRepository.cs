using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Account.Queries.GetProfile;
using Pencil.ContentManagement.Application.Features.Account.Queries.GetProfile.LoggedUser;
using Pencil.ContentManagement.Application.Features.Auth;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Persistence.Repositories;

public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserRepository(PencilDbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(dbContext)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<ApplicationUser?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<ApplicationUser>()
            .FirstOrDefaultAsync(a => a.Email.Equals(email), cancellationToken);
    }

    public async Task<ProfileDetailsDto?> GetProfileDetailsAsync(Expression<Func<ApplicationUser, bool>> wherePredicate, CancellationToken cancellationToken = default)
    {
        var profileDetails = await _dbContext.Set<ApplicationUser>()
                                         .Where(wherePredicate)
                                         .Include(u => u.Following)
                                         .Include(u => u.Followers)
                                         .Include(u => u.Posts)
                                         .Select(u => _mapper.Map<ProfileDetailsDto>(u))
                                         .FirstOrDefaultAsync(cancellationToken);

        profileDetails.SameUser = profileDetails.Id.Equals(AuthHelper.GetUserId(_httpContextAccessor).UserId);
      
        
        if (!profileDetails.SameUser)
        {
            var followers = await _dbContext.Set<ApplicationUser>()
                .Where(wherePredicate)
                .Include(u => u.Followers)
                .SelectMany(u => u.Followers.Select(f => f.FollowerId))
                .ToListAsync(cancellationToken);

            profileDetails.Followed = followers.Any(f => f.Equals(AuthHelper.GetUserId(_httpContextAccessor).UserId));
        }
        
        return profileDetails;
    }
    
    public async Task<LoggedUserProfileDetailsDto?> GetLoggedUserProfileDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<ApplicationUser>()
            .Where(u => u.Id.Equals(id))
            .Include(u => u.Following)
            .Include(u => u.Followers)
            .Include(u => u.Posts)
            .Select(u => _mapper.Map<LoggedUserProfileDetailsDto>(u))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbContext.Set<ApplicationUser>().AnyAsync(u => u.Id.Equals(id));
    }
}