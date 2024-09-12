using FluentValidation.Results;

namespace Pencil.ContentManagement.Application.Exceptions;

public class ValidationException : Exception
{
    public List<string> ValidationErrors { get; set; }

    public ValidationException(ValidationResult validationResult)
    {
        ValidationErrors = [];

        foreach (var validationError in validationResult.Errors)
        {
            ValidationErrors.Add(validationError.ErrorMessage);
        }
    }
}