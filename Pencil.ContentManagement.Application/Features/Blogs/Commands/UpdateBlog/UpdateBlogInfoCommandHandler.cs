using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Auth;
using Pencil.ContentManagement.Application.Resources;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Blogs.Commands.UpdateBlog;

public class UpdateBlogInfoCommandHandler : IRequestHandler<UpdateBlogInfoCommand, BaseResponse<string>>
{
    private readonly IBlogRepository _blogRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public UpdateBlogInfoCommandHandler(IBlogRepository blogRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _blogRepository = blogRepository ?? throw new ArgumentNullException(nameof(blogRepository));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<BaseResponse<string>> Handle(UpdateBlogInfoCommand request, CancellationToken cancellationToken)
    {
        var blog = await _blogRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (blog is null)
            return new BaseResponse<string>([$"There is no Blog with the specified Id: {request.Id}"],
                StatusCodes.Status404NotFound);
        
        if (!AuthHelper.IsUserAuthorized(_httpContextAccessor, blog.AuthorId))
            return new UnauthorizedResponse<string>();
        
        if (!string.IsNullOrEmpty(request.Username))
        {
            var exists = await _blogRepository.ExistsAsync(b => b.Username.Equals(request.Username) && b.Id != request.Id, cancellationToken);
            if(exists)
                return new ([Shared.UsernameAlreadyTaken]);
        }
        
        _mapper.Map(request, blog);
        await _blogRepository.SaveChangesAsync(cancellationToken);

        return new BaseResponse<string>(Shared.Success, StatusCodes.Status204NoContent);
    }
}