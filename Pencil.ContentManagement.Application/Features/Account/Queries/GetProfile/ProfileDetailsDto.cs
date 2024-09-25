namespace Pencil.ContentManagement.Application.Features.Account.Queries.GetProfile;

public record ProfileDetailsDto(
    Guid Id,
    string FirstName,
    string? LastName,
    string Username,
    string? Bio,
    string? PhotoPath,
    DateOnly? BirthDate,
    string? Gender,
    int FollowingCount,
    int FollowersCount);
