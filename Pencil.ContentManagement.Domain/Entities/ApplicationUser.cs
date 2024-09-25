using System.Security.Cryptography;

namespace Pencil.ContentManagement.Domain.Entities;
using BCrypt.Net;

public class ApplicationUser
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    
    public string Email { get; set; }

    private string _username;

    public string Username
    {
        get => _username;
        set => _username = string.IsNullOrEmpty(value)? $"{FirstName}{RandomNumberGenerator.GetHexString(10)}" : value;
    }

    private string _passwordHash;

    public string PasswordHash
    {
        get => _passwordHash;
        set => _passwordHash = BCrypt.HashPassword(value, 12);
    }

    public string? PhoneNumber { get; set; }
    
    
    public string? Bio { get; set; }
    
    public string? PhotoPath { get; set; }
    
    public DateOnly? BirthDate { get; set; }
    
    public string? Gender { get; set; }

    public bool Deactivated { get; set; }
    
    public bool SoftDeleted { get; set; }

    public IReadOnlyCollection<Following> Followers { get; }
    public IReadOnlyCollection<Following> Following { get; }
    
    public IReadOnlyCollection<Subscription> Subscriptions { get; }
    
    public IReadOnlyCollection<Post> Posts { get; }
    public IReadOnlyCollection<Blog> Blogs { get; }
    
    public bool ValidatePassword(string password)
    {
        return BCrypt.Verify(password, PasswordHash);
    }
    
}