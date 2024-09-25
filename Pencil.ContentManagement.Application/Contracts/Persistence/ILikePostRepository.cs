using Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Contracts.Persistence;

public interface ILikePostRepository : IAsyncRepository<Like<Post>>
{
    Task<Like<Post>> AddAsync(Guid postId, Guid userId, CancellationToken cancellationToken = default);
}