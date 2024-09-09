namespace Pencil.ContentManagement.Domain.Entities;

public class Like<T>
{
    public Guid ItemId { get; set; }
    public T Item { get; set; }
    
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }
}