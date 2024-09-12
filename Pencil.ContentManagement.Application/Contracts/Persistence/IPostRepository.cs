using Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Contracts.Persistence;

public interface IPostRepository : IAsyncRepository<Post>
{
    public Task<List<PostsDto>> GetPostsWithUserInfo(CancellationToken cancellationToken = default);
    
    public Task<List<PostsDto>> GetPostsByUserId(Guid id, CancellationToken cancellationToken = default);

    public Task SoftDelete(Post entity, CancellationToken cancellationToken = default);

}