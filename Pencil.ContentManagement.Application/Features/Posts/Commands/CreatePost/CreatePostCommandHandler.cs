using System.Security.Cryptography;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Auth;
using Pencil.ContentManagement.Application.Responses;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Features.Posts.Commands.CreatePost;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, BaseResponse<CreatePostDto>>
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAsyncRepository<Post> _postRepository;
    private readonly IBlogRepository _blogRepository;

    public CreatePostCommandHandler(IMapper mapper, IHttpContextAccessor httpContextAccessor, IAsyncRepository<Post> postRepository, IBlogRepository blogRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        _blogRepository = blogRepository ?? throw new ArgumentNullException(nameof(blogRepository));
    }

    public async Task<BaseResponse<CreatePostDto>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var result = AuthHelper.GetUserId(_httpContextAccessor);

        if (!result.Success)
        {
            return new BaseResponse<CreatePostDto>
            {
                StatusCode =  StatusCodes.Status400BadRequest,
                ValidationErrors = ["Can't Find the user"],
                Success = false
            };
        }

        if (request.BlogId.HasValue)
        {
            var blogExists = await _blogRepository.ExistsAsync(request.BlogId.Value);
            if (!blogExists)
            {
                return new BaseResponse<CreatePostDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ValidationErrors = [$"Incorrect Blog Id: {request.BlogId}"],
                    Success = false
                };
            }
               
        }
        
        var entity = _mapper.Map<Post>(request);

        entity.AuthorId = result.UserId;
        entity.Url = ((request.Title ?? request.Subtitle ?? string.Empty) + RandomNumberGenerator.GetHexString(10)).Replace(' ', '-');
        var post = await _postRepository.AddAsync(entity, cancellationToken);
        
        return new BaseResponse<CreatePostDto>
        {
            StatusCode = StatusCodes.Status201Created,
            Data = _mapper.Map<CreatePostDto>(post),
            Success = true
        };
    }
    
}