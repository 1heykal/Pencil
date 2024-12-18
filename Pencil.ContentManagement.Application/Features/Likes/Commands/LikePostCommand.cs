using MediatR;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Likes.Commands;

public class LikePostCommand : IRequest<BaseResponse<string>>
{
    public Guid PostId { get; set; }
}