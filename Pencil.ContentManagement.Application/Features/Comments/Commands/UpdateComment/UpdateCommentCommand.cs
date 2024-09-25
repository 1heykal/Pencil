using MediatR;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Comments.Commands.UpdateComment;

public class UpdateCommentCommand : IRequest<BaseResponse<string>>
{
    public Guid Id { get; set; }
    
    public string Content { get; set; }
    
}