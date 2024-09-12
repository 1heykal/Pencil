using MediatR;
using Pencil.ContentManagement.Application.Features.Posts.Commands.CreatePost;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Posts.Queries.GetPost;

public class GetPostByIdQuery : IRequest<BaseResponse<CreatePostDto>>
{
    public Guid Id { get; set; }
}