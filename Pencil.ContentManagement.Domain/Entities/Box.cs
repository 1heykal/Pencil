namespace Pencil.ContentManagement.Domain.Entities;

public class Box
{
    public Guid Id { get; set; }    
    public string Name { get; set; }
    public bool Private { get; set; }
    
    public string CreatorId { get; set; }
    public ApplicationUser Creator { get; set; }

    public IReadOnlyCollection<Post> Posts { get; set; }
}