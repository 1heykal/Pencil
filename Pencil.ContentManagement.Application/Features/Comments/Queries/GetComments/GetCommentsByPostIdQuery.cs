using MediatR;
using Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Comments.Queries.GetComments;

public class GetCommentsByPostIdQuery : IRequest<BaseResponse<IReadOnlyList<CommentsDto>>>
{
    public Guid PostId { get; set; }
}