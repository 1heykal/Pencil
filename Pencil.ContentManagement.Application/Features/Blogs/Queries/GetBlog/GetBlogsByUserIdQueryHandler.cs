using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Auth;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Blogs.Queries.GetBlog;

public class GetBlogsByUserIdQueryHandler : IRequestHandler<GetBlogsByUserIdQuery, BaseResponse<List<BlogInfoDto>>>
{
    private readonly IBlogRepository _blogRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepository _userRepository;

    public GetBlogsByUserIdQueryHandler(IBlogRepository blogRepository, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
    {
        _blogRepository = blogRepository ?? throw new ArgumentNullException(nameof(blogRepository));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        
    }
  
    
    public async Task<BaseResponse<List<BlogInfoDto>>> Handle(GetBlogsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var userExists = await _userRepository.ExistsAsync(AuthHelper.GetUserId(_httpContextAccessor).UserId);
        if (!userExists)
        {
            return new BaseResponse<List<BlogInfoDto>>([$"User with id: {AuthHelper.GetUserId(_httpContextAccessor).UserId} does not exist"], StatusCodes.Status404NotFound);
        }
        
        var blogs = await _blogRepository.GetBlogsByUserIdAsync(AuthHelper.GetUserId(_httpContextAccessor).UserId, cancellationToken);

        return new(blogs);
    }
}