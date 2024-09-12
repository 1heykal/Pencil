using MediatR;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;

public class GetPostsByUserIdQuery : IRequest<BaseResponse<List<PostsDto>>>
{
    public Guid UserId { get; set; }
}