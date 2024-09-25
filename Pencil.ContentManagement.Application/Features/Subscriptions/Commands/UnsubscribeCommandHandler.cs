using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Auth;
using Pencil.ContentManagement.Application.Features.Followings.Commands;
using Pencil.ContentManagement.Application.Resources;
using Pencil.ContentManagement.Application.Responses;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Features.Subscriptions.Commands;

public class UnsubscribeCommandHandler : IRequestHandler<UnsubscribeCommand, BaseResponse<string>>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAsyncRepository<Subscription> _subscriptionRepository;
    private readonly IBlogRepository _blogRepository;


    public UnsubscribeCommandHandler(IHttpContextAccessor httpContextAccessor, IAsyncRepository<Subscription> subscriptionRepository, IBlogRepository blogRepository)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _subscriptionRepository = subscriptionRepository ?? throw new ArgumentNullException(nameof(subscriptionRepository));
        _blogRepository = blogRepository ?? throw new ArgumentNullException(nameof(blogRepository));
    }

    public async Task<BaseResponse<string>> Handle(UnsubscribeCommand request, CancellationToken cancellationToken)
    {
        var userId = AuthHelper.GetUserId(_httpContextAccessor).UserId;
        
        // TODO: Shared Message
        if (!await _blogRepository.ExistsAsync(request.BlogId, cancellationToken))
            return new(Shared.UserNotFound, StatusCodes.Status404NotFound);

        var existed = await _subscriptionRepository.GetAsync(
            f => f.BlogId.Equals(request.BlogId) && f.UserId.Equals(userId), cancellationToken);
        
        if (existed is {Active: true})
        {
            existed.Active = false;
            await _subscriptionRepository.SaveChangesAsync(cancellationToken);
        }
        
        return new(Shared.Success, StatusCodes.Status204NoContent);
    }
}