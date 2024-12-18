using Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;

namespace Pencil.ContentManagement.Application.Features.Boxes.Queries;

public record BoxDto(string Name, Guid CreatorId, List<PostsDto> Posts);
