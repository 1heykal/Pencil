using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Blogs.Commands.DeleteBlog;

public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommand, BaseResponse<string>>
{
    private readonly IBlogRepository _blogRepository;

    public DeleteBlogCommandHandler(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository ?? throw new ArgumentNullException(nameof(blogRepository));
    }

    public async Task<BaseResponse<string>> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
    {
        var blog = await _blogRepository.GetByIdAsync(request.Id, cancellationToken);
        if (blog is null)
        {
            return new BaseResponse<string>
            {
                Success = false,
                StatusCode = StatusCodes.Status404NotFound,
                ValidationErrors = [$"There is no Blog with the specified Id: {request.Id}"]
            };
        }

        await _blogRepository.SoftDelete(blog, cancellationToken);

        return new BaseResponse<string>
        {
            StatusCode = StatusCodes.Status204NoContent,
            Message = "Blog Deleted Successfully."
        };
    }
}