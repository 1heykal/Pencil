namespace Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;

public class PostsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Content { get; set; }

    public DateTime PublishedOn { get; set; }
    
    public string BlogPhotoPath { get; set; } = string.Empty;
    public string BlogName { get; set; } = string.Empty;
    
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorPhotoPath { get; set; } = string.Empty;
}