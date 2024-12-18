using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Auth;
using Pencil.ContentManagement.Application.Resources;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Boxes.Queries;

public class GetBoxQueryHandler : IRequestHandler<GetBoxQuery, BaseResponse<BoxDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IBoxRepository _boxRepository;
    private readonly IMapper _mapper;

    public GetBoxQueryHandler(IUserRepository userRepository,
        IHttpContextAccessor httpContextAccessor, IBoxRepository boxRepository, IMapper mapper)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _boxRepository = boxRepository ?? throw new ArgumentNullException(nameof(boxRepository));
        _mapper = mapper?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<BaseResponse<BoxDto>> Handle(GetBoxQuery request, CancellationToken cancellationToken)
    {
        var userId = AuthHelper.GetUserId(_httpContextAccessor).UserId;
        
        if (!await _userRepository.ExistsAsync(userId))
            return new UnauthorizedResponse<BoxDto>();
        
        var userExists = await _userRepository.ExistsAsync(userId);
        
        if (!userExists)
            return new(false, Shared.UserNotFound, StatusCodes.Status404NotFound);

        var boxes = await _boxRepository.GetAllAsync(cancellationToken);
        
        if (boxes.Count == 0)
        {
            var box = await _boxRepository.AddAsync(new() { CreatorId = userId, Name = "Box" }, cancellationToken);
        }

        var existedBox = await _boxRepository.GetBoxDtoByUserIdAsync(userId, cancellationToken);
        
        return new(_mapper.Map<BoxDto>(existedBox));
    }
}