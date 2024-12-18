namespace Pencil.ContentManagement.Application.Features.Comments.Queries.GetComments;

public class CommentsDto
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime PublishedOn { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorPhotoPath { get; set; } = string.Empty;
    
    public string AuthorUserName { get; set; } = string.Empty;
    public int LikesCount { get; set; }
}