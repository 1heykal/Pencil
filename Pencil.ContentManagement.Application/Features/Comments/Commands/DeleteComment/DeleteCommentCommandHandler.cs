using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Auth;
using Pencil.ContentManagement.Application.Features.Comments.Commands.UpdateComment;
using Pencil.ContentManagement.Application.Responses;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Features.Comments.Commands.DeleteComment;

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, BaseResponse<string>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public DeleteCommentCommandHandler(ICommentRepository commentRepository, IHttpContextAccessor httpContextAccessor)
    {
        _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public async Task<BaseResponse<string>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdAsync(request.Id, cancellationToken);
        if (comment is null)
            return new BaseResponse<string>([$"There is no Comment with the specified Id: {request.Id}"],
                StatusCodes.Status404NotFound);
        
        if (!AuthHelper.IsUserAuthorized(_httpContextAccessor, comment.AuthorId))
            return new UnauthorizedResponse<string>();

        await _commentRepository.SoftDelete(comment, cancellationToken);

        return new BaseResponse<string>(true, "Comment Deleted Successfully.", StatusCodes.Status204NoContent);
    }
}