using System.Security.Cryptography;

namespace Pencil.ContentManagement.Domain.Entities;

public class Blog
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    private string _username;

    public string Username
    {
        get => _username;
        set
        {
            if (string.IsNullOrEmpty(value))
                _username = $"blog{RandomNumberGenerator.GetHexString(10)}";
            else
                _username = value;
        }
    }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public Guid AuthorId { get; set; }
    public ApplicationUser Author { get; set; }

    public string PhotoPath { get; set; }
    public bool SoftDeleted { get; set; }
    
    public IReadOnlyCollection<Post> Posts { get; set; }
    public IReadOnlyCollection<Subscription> Subscriptions { get; }
}