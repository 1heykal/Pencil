using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Auth;
using Pencil.ContentManagement.Application.Features.Comments.Commands.UpdateComment;
using Pencil.ContentManagement.Application.Resources;
using Pencil.ContentManagement.Application.Responses;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Features.Comments.Commands.UpdateComment;

public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, BaseResponse<string>>
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICommentRepository _commentRepository;

    public UpdateCommentCommandHandler(IMapper mapper, IHttpContextAccessor httpContextAccessor, ICommentRepository commentRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
    }

    public async Task<BaseResponse<string>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _commentRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (entity is null)
            return new ("Can't find the comment.", StatusCodes.Status404NotFound);
        
        if (!AuthHelper.IsUserAuthorized(_httpContextAccessor, entity.AuthorId))
            return new UnauthorizedResponse<string>();
             
        _mapper.Map(request, entity);
        await _commentRepository.SaveChangesAsync(cancellationToken);

        return new (Shared.Success, StatusCodes.Status204NoContent);
    }
    
}