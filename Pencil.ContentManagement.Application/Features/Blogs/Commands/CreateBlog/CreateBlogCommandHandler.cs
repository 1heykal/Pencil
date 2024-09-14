using System.Security.Cryptography;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Auth;
using Pencil.ContentManagement.Application.Responses;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Features.Blogs.Commands.CreateBlog;

public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, BaseResponse<CreatedBlogDto>>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<Blog> _blogRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public CreateBlogCommandHandler(IMapper mapper, IAsyncRepository<Blog> blogRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _blogRepository = blogRepository ?? throw new ArgumentNullException(nameof(blogRepository));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public async Task<BaseResponse<CreatedBlogDto>> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
        if (request.Username.IsNullOrEmpty())
        {
            request.Username = $"blog{RandomNumberGenerator.GetHexString(10)}";
        }
        
        var entity = _mapper.Map<Blog>(request);
        entity.AuthorId = AuthHelper.GetUserId(_httpContextAccessor).UserId;

        await _blogRepository.AddAsync(entity, cancellationToken);

        return new BaseResponse<CreatedBlogDto>()
        {
            StatusCode = StatusCodes.Status201Created,
            Data = _mapper.Map<CreatedBlogDto>(entity)
        };

    }
}