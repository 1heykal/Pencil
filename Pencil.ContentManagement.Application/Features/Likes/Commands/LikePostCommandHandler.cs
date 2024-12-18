using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Auth;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Likes.Commands;

public class LikePostCommandHandler : IRequestHandler<LikePostCommand, BaseResponse<string>>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILikePostRepository _likePostRepository;

    public LikePostCommandHandler(IHttpContextAccessor httpContextAccessor, ILikePostRepository likePostRepository)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _likePostRepository = likePostRepository ?? throw new ArgumentNullException(nameof(likePostRepository));
    }

    public async Task<BaseResponse<string>> Handle(LikePostCommand request, CancellationToken cancellationToken)
    {
        var userId = AuthHelper.GetUserId(_httpContextAccessor).UserId;

        var exists =
            await _likePostRepository.ExistsAsync(l => l.UserId.Equals(userId) && l.ItemId.Equals(request.PostId), cancellationToken);

        if (!exists)
            await _likePostRepository.AddAsync(request.PostId, userId, cancellationToken);
        
        return new();
    }
}