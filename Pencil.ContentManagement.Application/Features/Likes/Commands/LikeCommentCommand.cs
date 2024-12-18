using MediatR;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Likes.Commands;

public class LikeCommentCommand : IRequest<BaseResponse<string>>
{
    public Guid CommentId { get; set; }
}