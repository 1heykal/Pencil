using MediatR;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Blogs.Queries.GetBlog;

public class GetBlogInfoQuery : IRequest<BaseResponse<BlogInfoDto>>
{
    public Guid Id { get; set; }
}