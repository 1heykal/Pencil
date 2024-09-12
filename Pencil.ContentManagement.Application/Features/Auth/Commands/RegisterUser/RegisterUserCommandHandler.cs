using System.Security.Cryptography;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Responses;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Features.Auth.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, BaseResponse<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    
    public RegisterUserCommandHandler(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));;
    }

    public async Task<BaseResponse<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<string>();
        
        var user = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);

        if (user is not null)
        {
            response.ValidationErrors = [$"{request.Email} is already in use."];
            response.Success = false;
            response.StatusCode = StatusCodes.Status400BadRequest;
            return response;
        }
        
        var entity = _mapper.Map<ApplicationUser>(request);
        entity.Username = $"{request.FirstName}{RandomNumberGenerator.GetHexString(10)}";
        entity.SetPasswordHash(request.Password);
        user = await _userRepository.AddAsync(entity, cancellationToken);

        response.Data = AuthHelper.GenerateJwtToken(user.Id, _configuration);
        return response;
    }
}