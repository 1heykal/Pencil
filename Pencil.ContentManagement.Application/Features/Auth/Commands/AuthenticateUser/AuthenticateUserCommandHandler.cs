using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Responses;
using Pencil.ContentManagement.Domain.Entities;

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
        var response = new BaseResponse<string>();
        
        var user = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);

        if (user is null || !user.ValidatePassword(request.Password))
        {
            response.ValidationErrors = ["Incorrect Email or Password."];
            response.Success = false;
            return response;
        }
        
        response.Data = AuthHelper.GenerateJwtToken(user.Id, _configuration);
        return response;
    }
    
   
}