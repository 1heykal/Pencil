namespace Pencil.ContentManagement.Domain.Entities;

public class Following
{
    public Guid Id { get; set; }

    public Guid FollowerId { get; set; }
    public ApplicationUser Follower { get; set; }

    public Guid FollowedId { get; set; }
    public ApplicationUser Followed { get; set; }

    public DateTime FirstFollowedOn { get; set; } = DateTime.UtcNow;
}