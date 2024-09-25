using FluentValidation;
using Pencil.ContentManagement.Application.Resources;

namespace Pencil.ContentManagement.Application.Features.Auth.Commands.ChangePassword;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(c => c.OldPassword)
            .NotEmpty().WithMessage(Shared.PasswordRequired);
        
        RuleFor(c => c.NewPassword)
            .NotEmpty().WithMessage(Shared.PasswordRequired)
            .MinimumLength(8).WithMessage(Shared.PasswordMinLength)
            .Matches("[a-z]").WithMessage(Shared.PasswordLowercase)
            .Matches("[A-Z]").WithMessage(Shared.PasswordUppercase)
            .Matches(@"\d").WithMessage(Shared.PasswordDigit)
            .Matches(@"[@$!%*?&]").WithMessage(Shared.PasswordSpecialChar);
        
        RuleFor(c => c.ConfirmNewPassword)
            .Matches(c => c.NewPassword).WithMessage("Passwords don't match.");
    }
}