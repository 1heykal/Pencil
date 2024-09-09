namespace Pencil.ContentManagement.Domain.Entities;

public class Report<T>
{
    public Guid Id { get; set; }
    
    public string TypeOfIssue { get; set; }
    
    public string? Description { get; set; }
    public DateTime ReportDate { get; set; }
    
    public Guid ReporterId { get; set; }
    public ApplicationUser Reporter { get; set; }

    public Guid ItemId { get; set; }
    public T Item { get; set; }

    public bool Resolved { get; set; }
    public bool Archived { get; set; }
    
    public string ActionsTaken { get; set; }

}