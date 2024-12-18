using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Auth;
using Pencil.ContentManagement.Application.Resources;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Account.Queries.GetProfile.LoggedUser;

public class GetLoggedUserProfileDetailsQueryHandler : IRequestHandler<GetLoggedUserProfileDetailsQuery, BaseResponse<ProfileDetailsDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetLoggedUserProfileDetailsQueryHandler(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public async Task<BaseResponse<ProfileDetailsDto>> Handle(GetLoggedUserProfileDetailsQuery request, CancellationToken cancellationToken)
    {
        var userId = AuthHelper.GetUserId(_httpContextAccessor).UserId;
        
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        
        if(user is null)
            return new UnauthorizedResponse<ProfileDetailsDto>();
        
        var entity = await _userRepository.GetProfileDetailsAsync(u => u.Id.Equals(userId), cancellationToken);

        if (entity is null)
            return new(Shared.UserNotFound, StatusCodes.Status404NotFound);

        return new(entity);
    }
}