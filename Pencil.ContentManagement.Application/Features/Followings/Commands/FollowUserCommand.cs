using MediatR;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Followings.Commands;

public class FollowUserCommand : IRequest<BaseResponse<string>>
{
    public Guid FollowedId { get; set; }
}