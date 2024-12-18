using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Auth;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Posts.Commands.DeletePost;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, BaseResponse<string>>
{
    private readonly IPostRepository _postRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public DeletePostCommandHandler(IPostRepository postRepository, IHttpContextAccessor httpContextAccessor)
    {
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public async Task<BaseResponse<string>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.Id, cancellationToken);
        if (post is null)
            return new BaseResponse<string>([$"There is no Post with the specified Id: {request.Id}"],
                StatusCodes.Status404NotFound);
        
        if (!AuthHelper.IsUserAuthorized(_httpContextAccessor, post.AuthorId))
            return new UnauthorizedResponse<string>();

        await _postRepository.SoftDelete(post, cancellationToken);

        return new BaseResponse<string>(true, "Post Deleted Successfully.", StatusCodes.Status204NoContent);
    }
}