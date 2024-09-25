using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Auth;
using Pencil.ContentManagement.Application.Resources;
using Pencil.ContentManagement.Application.Responses;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Features.Blogs.Commands.CreateBlog;

public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, BaseResponse<CreatedBlogDto>>
{
    private readonly IMapper _mapper;
    private readonly IBlogRepository _blogRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepository _userRepository;


    public CreateBlogCommandHandler(IMapper mapper, IBlogRepository blogRepository,
        IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _blogRepository = blogRepository ?? throw new ArgumentNullException(nameof(blogRepository));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<BaseResponse<CreatedBlogDto>> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
        var userId = AuthHelper.GetUserId(_httpContextAccessor).UserId;
        
        if (!await _userRepository.ExistsAsync(userId))
            return new UnauthorizedResponse<CreatedBlogDto>();

        if (!string.IsNullOrEmpty(request.Username))
        {
            var exists = await _blogRepository.ExistsAsync(b => b.Username.Equals(request.Username), cancellationToken);
            if(exists)
                return new ([Shared.UsernameAlreadyTaken]);
        }
        
        var entity = _mapper.Map<Blog>(request);
        entity.AuthorId = userId;
        
        await _blogRepository.AddAsync(entity, cancellationToken);

        return new BaseResponse<CreatedBlogDto>(Shared.Success, _mapper.Map<CreatedBlogDto>(entity), StatusCodes.Status201Created);
    }
}