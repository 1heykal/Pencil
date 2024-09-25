using MediatR;
using Pencil.ContentManagement.Application.Features.Comments.Commands.CreateComment;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Comments.Commands.CreateComment;

public class CreateCommentCommand : IRequest<BaseResponse<CreateCommentDto>>
{
    public string Content { get; set; }
    
    public Guid PostId { get; set; }
    
}