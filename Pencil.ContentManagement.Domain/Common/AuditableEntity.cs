namespace Pencil.ContentManagement.Domain.Common;

public class AuditableEntity
{
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
}