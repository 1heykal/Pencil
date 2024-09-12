using System.Security.Cryptography;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Auth;
using Pencil.ContentManagement.Application.Features.Posts.Commands.CreatePost;
using Pencil.ContentManagement.Application.Responses;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Features.Posts.Commands.UpdatePost;

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, BaseResponse<string>>
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAsyncRepository<Post> _postRepository;

    public UpdatePostCommandHandler(IMapper mapper, IHttpContextAccessor httpContextAccessor, IAsyncRepository<Post> postRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
    }

    public async Task<BaseResponse<string>> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {

        var entity = await _postRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (entity is null)
        {
            return new BaseResponse<string>
            {
                ValidationErrors = ["Can't Find the post."],
                StatusCode = StatusCodes.Status404NotFound,
                Success = false
            };
        }
        
        var result = AuthHelper.GetUserId(_httpContextAccessor);

        if (!result.Success)
        {
            return new BaseResponse<string>
            {
                ValidationErrors = ["Can't Find the user"],
                StatusCode = StatusCodes.Status403Forbidden,
                Success = false
            };
        }

        if (!result.UserId.Equals(entity.AuthorId))
        {
            return new BaseResponse<string>
            {
                ValidationErrors = ["You don't have the authorization update this post."],
                StatusCode = StatusCodes.Status401Unauthorized,
                Success = false
            };
        }
             
        _mapper.Map(request, entity);
        await _postRepository.SaveChangesAsync(cancellationToken);

        return new (true, "Post Updated Successfully", StatusCodes.Status204NoContent);
    }
    
}