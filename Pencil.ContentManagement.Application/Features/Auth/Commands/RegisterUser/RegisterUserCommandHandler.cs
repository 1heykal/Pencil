using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Resources;
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
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<BaseResponse<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);

        if (user is not null)
            return new(false, Shared.EmailAlreadyInUse);
        
        var entity = _mapper.Map<ApplicationUser>(request);
        
        entity.PasswordHash = request.Password;
        entity.Username = string.Empty;
        
        user = await _userRepository.AddAsync(entity, cancellationToken);

        return new(AuthHelper.GenerateJwtToken(user.Id, _configuration));
    }
}