namespace Pencil.ContentManagement.Domain.Entities;

public class Blog
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public Guid AuthorId { get; set; }
    public ApplicationUser Author { get; set; }

    public string PhotoPath { get; set; }
    public bool SoftDeleted { get; set; }
    
    public IReadOnlyCollection<Post> Posts { get; set; }
    public IReadOnlyCollection<Subscription> Subscriptions { get; }
}