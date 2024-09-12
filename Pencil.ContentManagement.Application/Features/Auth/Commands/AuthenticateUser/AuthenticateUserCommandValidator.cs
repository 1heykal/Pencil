using FluentValidation;

namespace Pencil.ContentManagement.Application.Features.Auth.Commands.AuthenticateUser;

public class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
{
    public AuthenticateUserCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(c => c.Password)
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}