using MediatR;
using Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;
using Pencil.ContentManagement.Application.Responses;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Features.Boxes.Queries;

public class GetBoxQuery : IRequest<BaseResponse<BoxDto>>
{
    
}