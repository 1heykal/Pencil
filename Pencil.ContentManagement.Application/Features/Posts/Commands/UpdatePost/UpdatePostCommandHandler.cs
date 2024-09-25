using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Auth;
using Pencil.ContentManagement.Application.Resources;
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
            return new ("Can't find the post.", StatusCodes.Status404NotFound);
        
        if (!AuthHelper.IsUserAuthorized(_httpContextAccessor, entity.AuthorId))
            return new UnauthorizedResponse<string>();
             
        _mapper.Map(request, entity);
        await _postRepository.SaveChangesAsync(cancellationToken);

        return new (Shared.Success, StatusCodes.Status204NoContent);
    }
    
}