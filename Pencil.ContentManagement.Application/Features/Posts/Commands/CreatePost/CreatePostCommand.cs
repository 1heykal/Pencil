using MediatR;
using Pencil.ContentManagement.Application.Responses;

namespace Pencil.ContentManagement.Application.Features.Posts.Commands.CreatePost;

public class CreatePostCommand : IRequest<BaseResponse<CreatePostDto>>
{
    public string? Title { get; set; }
    
    public string? Subtitle { get; set; }
    
    public string Content { get; set; }

    public string? Type { get; set; }
    
    public List<string> Tags { get; set; }
    
    public Guid? BlogId { get; set; }
    
}