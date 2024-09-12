namespace Pencil.ContentManagement.Application.Features.Blogs.Commands.CreateBlog;

public record CreatedBlogDto
(
    Guid Id,
    string Name,
    string Username,
    string PhotoPath,
    DateTime CreatedAt);
