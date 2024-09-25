namespace Pencil.ContentManagement.Application.Features.Comments.Commands.CreateComment;

public class CreateCommentDto
{
    public Guid Id { get; set; } 
    
    public string Content { get; set; }
    
    public DateTime PublishedOn { get; set; }

    public bool SoftDeleted { get; set; }
    
    public Guid AuthorId { get; set; }
    public Guid PostId { get; set; }

}