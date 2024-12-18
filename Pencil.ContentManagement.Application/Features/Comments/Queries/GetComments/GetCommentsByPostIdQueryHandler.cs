using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Comments.Queries.GetComments;
using Pencil.ContentManagement.Application.Resources;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Comments.Queries.GetComments;

public class GetCommentsByPostIdQueryHandler : IRequestHandler<GetCommentsByPostIdQuery, BaseResponse<IReadOnlyList<CommentsDto>>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;


    public GetCommentsByPostIdQueryHandler(ICommentRepository commentRepository, IPostRepository postRepository)
    {
        _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
    }

    public async Task<BaseResponse<IReadOnlyList<CommentsDto>>> Handle(GetCommentsByPostIdQuery request, CancellationToken cancellationToken)
    {
        var postExists = await _postRepository.ExistsAsync(p => p.Id.Equals(request.PostId), cancellationToken);
        
        if (!postExists)
            return new([$"Can't find post with the specified id: {request.PostId}"], StatusCodes.Status404NotFound);

        var comments = await _commentRepository.GetAllDtosAsync(c => c.PostId.Equals(request.PostId), cancellationToken);
        return new(comments);
    }
}