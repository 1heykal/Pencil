using MediatR;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Account.Queries.GetProfile.LoggedUser;

public class GetLoggedUserProfileDetailsQuery : IRequest<BaseResponse<ProfileDetailsDto>>
{
}