using MediatR;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Blogs.Commands.DeleteBlog;

public class DeleteBlogCommand : IRequest<BaseResponse<string>>
{
    public Guid Id { get; set; }
}