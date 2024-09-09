namespace Pencil.ContentManagement.Domain.Entities;

public class Comment
{
    public Guid Id { get; set; }
    
    public string Content { get; set; }
    
    public Guid PostId { get; set; }
    public Post Post { get; set; }
    
    public Guid AuthorId { get; set; }
    public ApplicationUser Author { get; set; }
    
    public bool SoftDeleted { get; set; }

    public IReadOnlyCollection<Like<Comment>> Likes { get; }
    public IReadOnlyCollection<Report<Comment>> Reports { get; }
}