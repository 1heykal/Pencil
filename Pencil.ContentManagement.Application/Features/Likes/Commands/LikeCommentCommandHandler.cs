using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Auth;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Likes;

public class LikeCommentCommandHandler : IRequestHandler<LikeCommentCommand, BaseResponse<string>>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILikeCommentRepository _likeCommentRepository;

    public LikeCommentCommandHandler(IHttpContextAccessor httpContextAccessor, ILikeCommentRepository likeCommentRepository)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _likeCommentRepository = likeCommentRepository ?? throw new ArgumentNullException(nameof(likeCommentRepository));
    }

    public async Task<BaseResponse<string>> Handle(LikeCommentCommand request, CancellationToken cancellationToken)
    {
        var userId = AuthHelper.GetUserId(_httpContextAccessor).UserId;

        var exists =
            await _likeCommentRepository.ExistsAsync(l => l.UserId.Equals(userId) && l.ItemId.Equals(request.CommentId), cancellationToken);

        if (!exists)
            await _likeCommentRepository.AddAsync(request.CommentId, userId, cancellationToken);
        
        return new();
    }
}