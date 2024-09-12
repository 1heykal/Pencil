using MediatR;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Account.Commands.UpdateProfile;

public class UpdateProfileCommand : IRequest<BaseResponse<string>>
{
    public string FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public string Username { get; set; }
    
    public string? Bio { get; set; }
    
    public string? PhotoPath { get; set; }
    
    public DateOnly? BirthDate { get; set; }
    
    public string? Gender { get; set; }
    
}