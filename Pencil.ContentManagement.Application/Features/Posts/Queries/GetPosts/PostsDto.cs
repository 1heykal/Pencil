namespace Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;

public class PostsDto
{
    public Guid Id { get; set; }
    public string? Type { get; set; }

    public string Title { get; set; } = string.Empty;
    
    public string Url { get; set; } 
    public string Subtitle { get; set; } = string.Empty;
    public string Content { get; set; }

    public bool Liked { get; set; }
    
    public bool Saved { get; set; }

    public DateTime PublishedOn { get; set; }
    
    public string BlogPhotoPath { get; set; } = string.Empty;
    public string BlogName { get; set; } = string.Empty;
    
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorPhotoPath { get; set; } = string.Empty;

    public string AuthorUsername { get; set; }
    public string BlogUsername { get; set; }

    
    public int LikesCount { get; set; }
    public int CommentsCount { get; set; }

    public List<string> Tags { get; set; }
}