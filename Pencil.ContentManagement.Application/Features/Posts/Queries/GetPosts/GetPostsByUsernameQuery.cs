using MediatR;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;

public class GetPostsByUsernameQuery : IRequest<BaseResponse<IReadOnlyList<PostsDto>>>
{
    public string Username { get; set; }
}