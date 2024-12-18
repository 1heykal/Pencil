using System.Linq.Expressions;
using Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Contracts.Persistence;

public interface IPostRepository : IAsyncRepository<Post>
{
    Task<List<PostsDto>> GetPostsWithUserInfo(CancellationToken cancellationToken = default);
    
    Task<List<PostsDto>> GetPostsByUserId(Guid id, CancellationToken cancellationToken = default);

    Task SoftDelete(Post entity, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<PostsDto>> GetPostsDtoAsync(CancellationToken cancellationToken = default);

    Task<PostsDto?> GetPostsDtoAsync(Expression<Func<Post, bool>> wherePredicate,
        CancellationToken cancellationToken = default);
    
    Task<IReadOnlyList<PostsDto>> GetAllPostsDtoAsync(Expression<Func<Post, bool>> wherePredicate,
        CancellationToken cancellationToken = default);
    
    

    Task<List<PostsDto>> GetFeedPostsByUserId(Guid id,  CancellationToken cancellationToken = default);
    
    Task<List<PostsDto>> GetLikedPostsByUserId(Guid id,  CancellationToken cancellationToken = default);

    Task<List<PostsDto>> GetPostsByUserId(Guid id, Expression<Func<Post, bool>> wherePredicate,
        CancellationToken cancellationToken = default);


}