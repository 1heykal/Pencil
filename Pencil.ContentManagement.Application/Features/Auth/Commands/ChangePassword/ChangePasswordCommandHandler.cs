using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Resources;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Auth.Commands.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, BaseResponse<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ChangePasswordCommandHandler(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public async Task<BaseResponse<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var userId = AuthHelper.GetUserId(_httpContextAccessor).UserId;
        
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        
        if(user is null || !user.ValidatePassword(request.OldPassword))
            return new UnauthorizedResponse<string>();
        
        // if(!user.ValidatePassword(request.OldPassword))
        //     return new(Shared.IncorrectEmailOrPassword, StatusCodes.Status403Forbidden);

        user.PasswordHash = request.NewPassword;
        await _userRepository.SaveChangesAsync(cancellationToken);

        return new(["Password Changed Successfully!"], StatusCodes.Status204NoContent);
    }
}