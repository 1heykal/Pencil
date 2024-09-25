using MediatR;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Blogs.Queries.GetBlog;

public class GetBlogByUsernameQuery : IRequest<BaseResponse<BlogInfoDto>>
{
    public string Username { get; set; }
}