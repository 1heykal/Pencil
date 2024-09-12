using MediatR;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;

public class GetPostsQuery : IRequest<BaseResponse<List<PostsDto>>>
{
    
}