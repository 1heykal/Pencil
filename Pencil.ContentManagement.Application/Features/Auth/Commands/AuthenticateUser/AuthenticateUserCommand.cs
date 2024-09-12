using MediatR;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Auth.Commands.AuthenticateUser;

public class AuthenticateUserCommand : IRequest<BaseResponse<string>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}