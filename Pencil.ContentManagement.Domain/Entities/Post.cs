using System.Security.Cryptography;

namespace Pencil.ContentManagement.Domain.Entities;

public class Post
{
    public Guid Id { get; }

    public string? Type { get; set; } = string.Empty;
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    
    public string Content { get; set; }
    public DateTime PublishedOn { get; set; } = DateTime.UtcNow;

    public bool Archived { get; set; }
    public bool SoftDeleted { get; set; }

    private string _url;
    public string Url
    {
        get => _url;
        set => _url = !string.IsNullOrEmpty(value)
            ? value : ((Title?[..Math.Min(Title.Length, 15)] ?? Subtitle?[..Math.Min(Subtitle.Length, 15)] ?? string.Empty) + RandomNumberGenerator.GetHexString(10)).Replace(' ', '-').Replace( '?','-');
    }

    public Guid? BlogId { get; set; }
    public Blog? Blog { get; set; }
    public Guid AuthorId { get; set; }
    public ApplicationUser Author { get; set; }
    public ICollection<Tag> Tags { get; set; }
    public IReadOnlyCollection<Comment> Comments { get;}
    public IReadOnlyCollection<Like<Post>> Likes { get;}
    
    public IReadOnlyCollection<Report<Post>> Reports { get; }
       
}

