using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Blogs.Queries.GetBlog;

public class GetBlogByUsernameQueryHandler : IRequestHandler<GetBlogByUsernameQuery, BaseResponse<BlogInfoDto>>
{
    private readonly IBlogRepository _blogRepository;
    private readonly IMapper _mapper;

    public GetBlogByUsernameQueryHandler(IBlogRepository blogRepository, IMapper mapper)
    {
        _blogRepository = blogRepository ?? throw new ArgumentNullException(nameof(blogRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<BaseResponse<BlogInfoDto>> Handle(GetBlogByUsernameQuery request, CancellationToken cancellationToken)
    {
        var blog = await _blogRepository.GetBlogInfo(blog => blog.Username.Equals(request.Username), cancellationToken);

        if (blog is null)
            return new ([$"Can't find post with the specified username: {request.Username}"], StatusCodes.Status404NotFound);
            
        return new (_mapper.Map<BlogInfoDto>(blog));
    }
}