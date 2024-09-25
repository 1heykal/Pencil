using System.Linq.Expressions;
using Pencil.ContentManagement.Application.Features.Account.Queries.GetProfile;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Contracts.Persistence;

public interface IUserRepository : IAsyncRepository<ApplicationUser>
{ 
    Task<ApplicationUser?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    
    Task<ProfileDetailsDto?> GetProfileDetailsAsync(Expression<Func<ApplicationUser, bool>> wherePredicate, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Guid id);
}