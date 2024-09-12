using FluentValidation;

namespace Pencil.ContentManagement.Application.Features.Auth.Commands.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty();

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("{PropertyName} can't be Empty")
            .EmailAddress().WithMessage("Invalid Email Address.");
        
        RuleFor(c => c.Password)
            .NotEmpty().WithMessage("{PropertyName} can't be Empty")
            .MinimumLength(8).WithMessage("{PropertyName} must be at least 8 characters long.");

        RuleFor(c => c.ConfirmPassword)
            .Matches(c => c.Password).WithMessage("Passwords don't match.");
        
    }
}