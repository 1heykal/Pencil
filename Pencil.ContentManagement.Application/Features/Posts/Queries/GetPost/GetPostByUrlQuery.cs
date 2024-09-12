using MediatR;
using Pencil.ContentManagement.Application.Features.Posts.Commands.CreatePost;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Posts.Queries.GetPost;

public class GetPostByUrlQuery : IRequest<BaseResponse<CreatePostDto>>
{
    public string Url { get; set; }
}