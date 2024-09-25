using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Resources;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Account.Queries.GetProfile;

public class GetProfileDetailsQueryHandler : IRequestHandler<GetProfileDetailsQuery, BaseResponse<ProfileDetailsDto>>
{
    private readonly IUserRepository _userRepository;

    public GetProfileDetailsQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<BaseResponse<ProfileDetailsDto>> Handle(GetProfileDetailsQuery request, CancellationToken cancellationToken)
    {
        var entity = await _userRepository.GetProfileDetailsAsync(u => u.Username.Equals(request.Username), cancellationToken);

        if (entity is null)
            return new(Shared.UserNotFound, StatusCodes.Status404NotFound);

        return new(entity);
    }
}