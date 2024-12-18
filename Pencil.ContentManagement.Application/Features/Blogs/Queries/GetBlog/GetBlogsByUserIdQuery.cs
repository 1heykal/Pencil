using MediatR;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Blogs.Queries.GetBlog;

public class GetBlogsByUserIdQuery : IRequest<BaseResponse<List<BlogInfoDto>>>
{
    
}