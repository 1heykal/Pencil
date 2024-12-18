using MediatR;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Likes.Commands;

public class UnlikePostCommand : IRequest<BaseResponse<string>>
{
    public Guid PostId { get; set; }
}