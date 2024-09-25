using MediatR;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Comments.Commands.DeleteComment;

public class DeleteCommentCommand : IRequest<BaseResponse<string>>
{
    public Guid Id { get; set; }
}