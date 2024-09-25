using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Auth;
using Pencil.ContentManagement.Application.Responses;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Features.Comments.Commands.CreateComment;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, BaseResponse<CreateCommentDto>>
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;

    public CreateCommentCommandHandler(IMapper mapper, IHttpContextAccessor httpContextAccessor, ICommentRepository commentRepository, IPostRepository postRepository, IUserRepository userRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<BaseResponse<CreateCommentDto>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {

        var postExists = await _postRepository.ExistsAsync(p => p.Id.Equals(request.PostId), cancellationToken);
        
        if (!postExists)
                return new BaseResponse<CreateCommentDto>([$"Incorrect Post Id: {request.PostId}"]);
        
        var entity = _mapper.Map<Comment>(request);
        
        var userId = AuthHelper.GetUserId(_httpContextAccessor).UserId;
        if (!await _userRepository.ExistsAsync(userId))
            return new UnauthorizedResponse<CreateCommentDto>();

        entity.AuthorId = userId;
        
        var comment = await _commentRepository.AddAsync(entity, cancellationToken);
        
        return new BaseResponse<CreateCommentDto>
        {
            StatusCode = StatusCodes.Status201Created,
            Data = _mapper.Map<CreateCommentDto>(comment)
            
        };
    }
    
}