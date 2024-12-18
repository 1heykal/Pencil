namespace Pencil.ContentManagement.Application.Features.Account.Queries.GetProfile.LoggedUser;

public record LoggedUserProfileDetailsDto(
    string Id,
    string FirstName,
    string? LastName,
    string Username,
    string? Bio,
    string? PhotoPath,
    DateOnly? BirthDate,
    string? Gender,
    int FollowingCount,
    int FollowersCount,
    int PostsCount);
