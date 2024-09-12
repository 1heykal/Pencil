using MediatR;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Blogs.Commands.CreateBlog;

public class CreateBlogCommand : IRequest<BaseResponse<CreatedBlogDto>>
{
    public string Name { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PhotoPath { get; set; } = string.Empty;
}