using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Auth;
using Pencil.ContentManagement.Application.Features.Likes.Commands;
using Pencil.ContentManagement.Application.Resources;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Boxes.Commands;

public class UnSavePostToBoxCommandHandler : IRequestHandler<UnSavePostToBoxCommand, BaseResponse<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IBoxRepository _boxRepository;
    private readonly IPostRepository _postRepository;

    public UnSavePostToBoxCommandHandler(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor,
        IBoxRepository boxRepository, IPostRepository postRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _boxRepository = boxRepository ?? throw new ArgumentNullException(nameof(boxRepository));
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
    }

    public async Task<BaseResponse<string>> Handle(UnSavePostToBoxCommand request, CancellationToken cancellationToken)
    {
        var userId = AuthHelper.GetUserId(_httpContextAccessor).UserId;
        
        if (!await _userRepository.ExistsAsync(userId))
            return new UnauthorizedResponse<string>();
        
        var userExists = await _userRepository.ExistsAsync(userId);
        
        if (!userExists)
            return new(false, Shared.UserNotFound, StatusCodes.Status404NotFound);
        
        var post = await _postRepository.GetByIdAsync(request.PostId, cancellationToken);
        if (post is null)
            return new BaseResponse<string>([$"There is no Post with the specified Id: {request.PostId}"],
                StatusCodes.Status404NotFound);
        
        var existedBox = await _boxRepository.GetAsync(b => b.CreatorId.Equals(userId), cancellationToken);
        existedBox.Posts.Remove(post);
        await _boxRepository.SaveChangesAsync(cancellationToken);
        return new(Shared.Success, string.Empty, StatusCodes.Status204NoContent);
        
    }
}