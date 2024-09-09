namespace Pencil.ContentManagement.Domain.Entities;

public class Tag
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public IReadOnlyCollection<Post> Posts { get; set; }
}