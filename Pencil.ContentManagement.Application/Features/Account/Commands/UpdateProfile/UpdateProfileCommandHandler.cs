using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Auth;
using Pencil.ContentManagement.Application.Resources;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Account.Commands.UpdateProfile;

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, BaseResponse<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public UpdateProfileCommandHandler(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<BaseResponse<string>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var userId = AuthHelper.GetUserId(_httpContextAccessor).UserId;
        
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        
        if(user is null)
            return new UnauthorizedResponse<string>();
        
        if (!string.IsNullOrEmpty(request.Username))
        {
            var exists = await _userRepository.ExistsAsync(b => b.Username.Equals(request.Username) && b.Id != userId, cancellationToken);
            if(exists)
                return new ([Shared.UsernameAlreadyTaken]);
        }
        
        _mapper.Map(request, user);
        await _userRepository.SaveChangesAsync(cancellationToken);
        
        return new (Shared.ProfileUpdated,StatusCodes.Status204NoContent);
    }
}