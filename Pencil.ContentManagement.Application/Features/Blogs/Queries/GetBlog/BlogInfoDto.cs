namespace Pencil.ContentManagement.Application.Features.Blogs.Queries.GetBlog;

public class BlogInfoDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Username { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string PhotoPath { get; set; } = string.Empty;
    
    public int PostsCount { get; set; }
    public int SubscriptionsCount { get; set; }

    public string AuthorName { get; set; }
    public string? AuthorPhotoPath { get; set; }

    public bool IsCurrentUserSubscribed { get; set; }
}