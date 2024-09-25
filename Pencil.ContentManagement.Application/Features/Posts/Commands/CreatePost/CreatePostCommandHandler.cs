using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
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
    private readonly IUserRepository _userRepository;

    public CreatePostCommandHandler(IMapper mapper, IHttpContextAccessor httpContextAccessor, IAsyncRepository<Post> postRepository, IBlogRepository blogRepository, IUserRepository userRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        _blogRepository = blogRepository ?? throw new ArgumentNullException(nameof(blogRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<BaseResponse<CreatePostDto>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        if (request.BlogId.HasValue)
        {
            var blogExists = await _blogRepository.ExistsAsync(request.BlogId.Value, cancellationToken);
            if (!blogExists)
                return new BaseResponse<CreatePostDto>([$"Incorrect Blog Id: {request.BlogId}"]);
        }
        
        var entity = _mapper.Map<Post>(request);
        
        var userId = AuthHelper.GetUserId(_httpContextAccessor).UserId;
        if (!await _userRepository.ExistsAsync(userId))
            return new UnauthorizedResponse<CreatePostDto>();

        entity.AuthorId = userId;
        
        var post = await _postRepository.AddAsync(entity, cancellationToken);
        
        return new BaseResponse<CreatePostDto>
        {
            StatusCode = StatusCodes.Status201Created,
            Data = _mapper.Map<CreatePostDto>(post)
            
        };
    }
    
}