using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Auth;
using Pencil.ContentManagement.Application.Resources;
using Pencil.ContentManagement.Application.Responses;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Features.Subscriptions.Commands;

public class SubscribeCommandHandler : IRequestHandler<SubscribeCommand, BaseResponse<string>>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAsyncRepository<Subscription> _subscriptionRepository;
    private readonly IBlogRepository _blogRepository;
    private readonly IMapper _mapper;

    public SubscribeCommandHandler(IHttpContextAccessor httpContextAccessor, IMapper mapper, IAsyncRepository<Subscription> subscriptionRepository, IBlogRepository blogRepository)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _subscriptionRepository = subscriptionRepository ?? throw new ArgumentNullException(nameof(subscriptionRepository));
        _blogRepository = blogRepository ?? throw new ArgumentNullException(nameof(blogRepository));
    }

    public async Task<BaseResponse<string>> Handle(SubscribeCommand request, CancellationToken cancellationToken)
    {
        var userId = AuthHelper.GetUserId(_httpContextAccessor).UserId;
        
        // TODO: Shared Message
        if (!await _blogRepository.ExistsAsync(request.BlogId, cancellationToken))
            return new(Shared.UserNotFound, StatusCodes.Status404NotFound);
                
        var entity = _mapper.Map<Subscription>(request);
        entity.UserId = userId;

        var existed = await _subscriptionRepository.GetAsync(
            s => s.BlogId.Equals(request.BlogId) && s.UserId.Equals(userId), cancellationToken);
      
        if(existed is null)
        {
            await _subscriptionRepository.AddAsync(entity, cancellationToken);
        }
        else if (!existed.Active)
        {
            existed.Active = true;
            await _subscriptionRepository.SaveChangesAsync(cancellationToken);
        }
        
        return new(Shared.Success, StatusCodes.Status201Created);
    }
}