namespace Pencil.ContentManagement.Domain.Entities;

public class Box
{
    public Guid Id { get; set; }    
    public string Name { get; set; }
    public bool Public { get; set; }
    
    public Guid CreatorId { get; set; }
    public ApplicationUser Creator { get; set; }

    public ICollection<Post> Posts { get; set; }
}