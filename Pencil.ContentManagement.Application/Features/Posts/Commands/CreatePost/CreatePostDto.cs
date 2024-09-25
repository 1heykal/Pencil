namespace Pencil.ContentManagement.Application.Features.Posts.Commands.CreatePost;

public class CreatePostDto
{
    public Guid Id { get; set; } 
    
    public string? Title { get; set; }
    
    public string? Subtitle { get; set; }
    public string Content { get; set; }
    
    public DateTime PublishedOn { get; set; } = DateTime.UtcNow;

    public bool Archived { get; set; }
    public bool SoftDeleted { get; set; }
    public string Url { get; set; }

    public Guid? BlogId { get; set; }
    
    public Guid AuthorId { get; set; }
}