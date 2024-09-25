using MediatR;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Blogs.Commands.UpdateBlog;

public class UpdateBlogInfoCommand : IRequest<BaseResponse<string>>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string PhotoPath { get; set; }
}