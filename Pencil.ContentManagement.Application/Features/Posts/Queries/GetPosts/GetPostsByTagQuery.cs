using MediatR;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;

public class GetPostsByTagQuery : IRequest<BaseResponse<IReadOnlyList<PostsDto>>>
{
    public string Name { get; set; }
}