using FluentValidation;
using Pencil.ContentManagement.Application.Resources;

namespace Pencil.ContentManagement.Application.Features.Auth.Commands.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty();

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email can't be Empty")
            .EmailAddress().WithMessage("Invalid Email Address.");

        RuleFor(c => c.Password)
            .NotEmpty().WithMessage(Shared.PasswordRequired)
            .MinimumLength(8).WithMessage(Shared.PasswordMinLength)
            .Matches("[a-z]").WithMessage(Shared.PasswordLowercase)
            .Matches("[A-Z]").WithMessage(Shared.PasswordUppercase)
            .Matches(@"\d").WithMessage(Shared.PasswordDigit)
            .Matches(@"[@$!%*?&]").WithMessage(Shared.PasswordSpecialChar);
        
        RuleFor(c => c.ConfirmPassword)
            .Matches(c => c.Password).WithMessage("Passwords don't match.");
        
    }
}