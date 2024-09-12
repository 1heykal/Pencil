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
        var response = new BaseResponse<string>();
        var result = AuthHelper.GetUserId(_httpContextAccessor);
        if (!result.Success)
        {
            response.ValidationErrors = ["Can't Find the user"];
            response.StatusCode = StatusCodes.Status400BadRequest;
            return response;
        }

        var user = await _userRepository.GetByIdAsync(result.UserId, cancellationToken);
        _mapper.Map(request, user);
        await _userRepository.SaveChangesAsync(cancellationToken);
        response.StatusCode = StatusCodes.Status204NoContent;
        return response;
    }
}