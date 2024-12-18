using MediatR;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Boxes.Commands;

public class UnSavePostToBoxCommand : IRequest<BaseResponse<string>>
{
    public Guid PostId { get; set; }
}