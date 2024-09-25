using MediatR;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Subscriptions.Commands;

public class SubscribeCommand : IRequest<BaseResponse<string>>
{
    public Guid BlogId { get; set; }
}