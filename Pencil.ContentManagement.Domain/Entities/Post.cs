namespace Pencil.ContentManagement.Domain.Entities;

public class Post
{
    public Guid Id { get; } 
    
    public string? Title { get; set; }
    
    public string? Subtitle { get; set; }
    public string Content { get; set; }
    
    public DateTime PublishedOn { get; set; } = DateTime.UtcNow;

    public bool Archived { get; set; }
    public bool SoftDeleted { get; set; }
    public string Url { get; set; }

    public Guid? BlogId { get; set; }
    public Blog? Blog { get; set; }
    
    public Guid AuthorId { get; set; }
    public ApplicationUser Author { get; set; }
    
    public IReadOnlyCollection<Tag> Tags { get; }
    public IReadOnlyCollection<Comment> Comments { get;}
    public IReadOnlyCollection<Like<Post>> Likes { get;}
    public IReadOnlyCollection<Report<Post>> Reports { get; }
       
}

