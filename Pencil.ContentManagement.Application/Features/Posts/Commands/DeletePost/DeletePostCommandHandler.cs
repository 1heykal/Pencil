using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Posts.Commands.UpdatePost;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, BaseResponse<string>>
{
    private readonly IPostRepository _postRepository;

    public DeletePostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
    }

    public async Task<BaseResponse<string>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.Id, cancellationToken);
        if (post is null)
        {
            return new BaseResponse<string>
            {
                Success = false,
                StatusCode = StatusCodes.Status404NotFound,
                ValidationErrors = [$"There is no Post with the specified Id: {request.Id}"]
            };
        }

        await _postRepository.SoftDelete(post, cancellationToken);

        return new BaseResponse<string>
        {
            StatusCode = StatusCodes.Status204NoContent,
            Message = "Post Deleted Successfully."
        };
    }
}