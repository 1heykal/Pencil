namespace Pencil.ContentManagement.Domain.Entities;

public class Subscription
{
    public Guid Id { get; set; }

    public string Type { get; set; }
    
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }

    public Guid BlogId { get; set; }
    public Blog Blog { get; set; }

    public DateTime SubscribedOn { get; set; } = DateTime.UtcNow;

    public bool Active { get; set; }
}