using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Auth;
using Pencil.ContentManagement.Application.Resources;
using Pencil.ContentManagement.Application.Responses;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Features.Followings.Commands;

public class FollowUserCommandHandler : IRequestHandler<FollowUserCommand, BaseResponse<string>>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IFollowingRepository _followingRepository;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public FollowUserCommandHandler(IHttpContextAccessor httpContextAccessor, IFollowingRepository followingRepository, IMapper mapper, IUserRepository userRepository)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _followingRepository = followingRepository ?? throw new ArgumentNullException(nameof(followingRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<BaseResponse<string>> Handle(FollowUserCommand request, CancellationToken cancellationToken)
    {
        var userId = AuthHelper.GetUserId(_httpContextAccessor).UserId;

        if (userId.Equals(request.FollowedId))
            return new(["Users can't follow themselves."]);


        if (!await _userRepository.ExistsAsync(request.FollowedId))
            return new(Shared.UserNotFound, StatusCodes.Status404NotFound);
                
        var entity = _mapper.Map<Following>(request);
        entity.FollowerId = userId;

        var existed = await _followingRepository.GetAsync(
            f => f.FollowedId.Equals(request.FollowedId) && f.FollowerId.Equals(userId), cancellationToken);
      
        if(existed is null)
        {
            await _followingRepository.AddAsync(entity, cancellationToken);
        }
        else if (existed.SoftDeleted)
        {
            existed.SoftDeleted = false;
            await _followingRepository.SaveChangesAsync(cancellationToken);
        }
        
        return new(Shared.Success, StatusCodes.Status201Created);
    }
}