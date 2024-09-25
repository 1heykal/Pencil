using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Contracts.Persistence;

public interface ICommentRepository : IAsyncRepository<Comment>
{
    Task SoftDelete(Comment entity, CancellationToken cancellationToken = default);
}