using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Contracts.Persistence;

public interface ILikeCommentRepository : IAsyncRepository<Like<Comment>>
{
    Task<Like<Comment>> AddAsync(Guid commentId, Guid userId, CancellationToken cancellationToken = default);
}