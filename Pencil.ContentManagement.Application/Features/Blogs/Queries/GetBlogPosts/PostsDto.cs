namespace Pencil.ContentManagement.Application.Features.Blogs.Queries.GetBlogPosts;

public class BlogPostsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Content { get; set; }

    public DateTime PublishedOn { get; set; }
    
    public string AuthorUsername { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorPhotoPath { get; set; } = string.Empty;
}