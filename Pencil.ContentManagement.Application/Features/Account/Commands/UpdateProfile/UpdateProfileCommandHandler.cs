using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Auth;
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
        var result = AuthHelper.GetUserId(_httpContextAccessor);
        
        var user = await _userRepository.GetByIdAsync(result.UserId, cancellationToken);
        
        _mapper.Map(request, user);
        await _userRepository.SaveChangesAsync(cancellationToken);
        
        return new ("Profile updated successfully!",StatusCodes.Status204NoContent);
    }
}