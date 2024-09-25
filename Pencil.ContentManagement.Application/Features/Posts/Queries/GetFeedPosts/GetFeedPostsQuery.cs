using MediatR;
using Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Posts.Queries.GetFeedPosts;

public class GetFeedPostsQuery : IRequest<BaseResponse<List<PostsDto>>>
{
    
}