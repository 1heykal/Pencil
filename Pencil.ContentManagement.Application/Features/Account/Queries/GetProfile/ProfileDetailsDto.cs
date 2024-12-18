namespace Pencil.ContentManagement.Application.Features.Account.Queries.GetProfile;

public class ProfileDetailsDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string Username { get; set; }
    public string? Bio { get; set; }
    public string? PhotoPath { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string? Gender { get; set; }
    public int FollowingCount { get; set; }
    public int FollowersCount { get; set; }
    public int PostsCount { get; set; }
    public bool SameUser { get; set; }
    public bool Followed { get; set; }
}