using MediatR;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Posts.Commands.DeletePost;

public class DeletePostCommand : IRequest<BaseResponse<string>>
{
    public Guid Id { get; set; }
}