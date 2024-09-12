using MediatR;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Posts.Commands.UpdatePost;

public class UpdatePostCommand : IRequest<BaseResponse<string>>
{
    public Guid Id { get; set; }
    
    public string? Title { get; set; }
    
    public string? Subtitle { get; set; }
    
    public string Content { get; set; }
    
}