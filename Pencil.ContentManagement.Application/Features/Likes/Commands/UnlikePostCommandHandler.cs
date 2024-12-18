using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Auth;
using Pencil.ContentManagement.Application.Resources;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Likes.Commands;

public class UnlikePostCommandHandler : IRequestHandler<UnlikePostCommand, BaseResponse<string>>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILikePostRepository _likePostRepository;

    public UnlikePostCommandHandler(IHttpContextAccessor httpContextAccessor, ILikePostRepository likePostRepository)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _likePostRepository = likePostRepository ?? throw new ArgumentNullException(nameof(likePostRepository));
    }

    public async Task<BaseResponse<string>> Handle(UnlikePostCommand request, CancellationToken cancellationToken)
    {
        var userId = AuthHelper.GetUserId(_httpContextAccessor).UserId;

        var existed =
            await _likePostRepository.GetAsync(l => l.UserId.Equals(userId) && l.ItemId.Equals(request.PostId), cancellationToken);

        if (existed is not null)
            await _likePostRepository.DeleteAsync(existed, cancellationToken);
        
        return new(Shared.Success, string.Empty, StatusCodes.Status204NoContent);
    }
}