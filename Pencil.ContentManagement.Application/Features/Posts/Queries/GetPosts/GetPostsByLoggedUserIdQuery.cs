using MediatR;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;

public class GetPostsByLoggedUserIdQuery : IRequest<BaseResponse<List<PostsDto>>>
{
}