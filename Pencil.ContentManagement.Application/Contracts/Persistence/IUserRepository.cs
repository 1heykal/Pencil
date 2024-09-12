using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Contracts.Persistence;

public interface IUserRepository : IAsyncRepository<ApplicationUser>
{ 
    Task<ApplicationUser?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Guid id);
}