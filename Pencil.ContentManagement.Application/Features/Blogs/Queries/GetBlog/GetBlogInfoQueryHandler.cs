using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Blogs.Queries.GetBlog;

public class GetBlogInfoQueryHandler : IRequestHandler<GetBlogInfoQuery, BaseResponse<BlogInfoDto>>
{
    private readonly IBlogRepository _blogRepository;

    public GetBlogInfoQueryHandler(IBlogRepository blogRepository, IMapper mapper)
    {
        _blogRepository = blogRepository ?? throw new ArgumentNullException(nameof(blogRepository));
    }
    
    public async Task<BaseResponse<BlogInfoDto>> Handle(GetBlogInfoQuery request, CancellationToken cancellationToken)
    {
        
        var blog = await _blogRepository.GetBlogInfo(request.Id, cancellationToken);

        if (blog is null)
        {
            return new BaseResponse<BlogInfoDto>
            {
                Success = false,
                StatusCode = StatusCodes.Status404NotFound,
                ValidationErrors = [$"There is no blog with the id: {request.Id}"]
            };
        }
        
        return new BaseResponse<BlogInfoDto>(blog);

    }
}