using MediatR;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Posts.Commands.UpdatePost;

public class DeletePostCommand : IRequest<BaseResponse<string>>
{
    public Guid Id { get; set; }
}