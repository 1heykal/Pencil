using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Resources;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Auth.Commands.AuthenticateUser;

public class AuthenticateUserCommandHandler: IRequestHandler<AuthenticateUserCommand, BaseResponse<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    
    public AuthenticateUserCommandHandler(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<BaseResponse<string>> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);

        if (user is null || !user.ValidatePassword(request.Password))
            return new(Shared.IncorrectEmailOrPassword, StatusCodes.Status403Forbidden);
        
        return new (Shared.Success, AuthHelper.GenerateJwtToken(user.Id, _configuration));
    }
    
   
}