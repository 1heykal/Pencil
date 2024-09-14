using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Auth;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Blogs.Commands.DeleteBlog;

public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommand, BaseResponse<string>>
{
    private readonly IBlogRepository _blogRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DeleteBlogCommandHandler(IBlogRepository blogRepository, IHttpContextAccessor httpContextAccessor)
    {
        _blogRepository = blogRepository ?? throw new ArgumentNullException(nameof(blogRepository));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public async Task<BaseResponse<string>> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
    {
        var blog = await _blogRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (blog is null)
            return new BaseResponse<string>([$"There is no Blog with the specified Id: {request.Id}"],
                StatusCodes.Status404NotFound);
        
        if (!AuthHelper.IsUserAuthorized(_httpContextAccessor, blog.AuthorId))
            return new UnauthorizedResponse<string>();

        await _blogRepository.SoftDelete(blog, cancellationToken);

        return new BaseResponse<string>(true, "Blog Deleted Successfully.", StatusCodes.Status204NoContent);
    }
}