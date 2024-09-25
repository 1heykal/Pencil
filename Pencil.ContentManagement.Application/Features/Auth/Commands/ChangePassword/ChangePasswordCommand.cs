using MediatR;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Auth.Commands.ChangePassword;

public class ChangePasswordCommand : IRequest<BaseResponse<string>>
{
    public string OldPassword { get; set; }

    public string NewPassword { get; set; }

    public string ConfirmNewPassword { get; set; }
}