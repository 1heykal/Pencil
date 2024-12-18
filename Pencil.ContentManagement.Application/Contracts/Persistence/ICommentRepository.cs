using System.Linq.Expressions;
using Pencil.ContentManagement.Application.Features.Comments.Queries.GetComments;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Contracts.Persistence;

public interface ICommentRepository : IAsyncRepository<Comment>
{
    Task SoftDelete(Comment entity, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<CommentsDto>> GetAllDtosAsync(Expression<Func<Comment, bool>> wherePredicate,
        CancellationToken cancellationToken = default);
}