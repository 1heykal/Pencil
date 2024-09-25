using MediatR;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Account.Queries.GetProfile;

public class GetProfileDetailsQuery : IRequest<BaseResponse<ProfileDetailsDto>>
{
    public string Username { get; set; }
}