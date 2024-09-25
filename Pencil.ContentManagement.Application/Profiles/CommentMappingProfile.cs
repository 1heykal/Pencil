using AutoMapper;
using Pencil.ContentManagement.Application.Features.Comments.Commands.CreateComment;
using Pencil.ContentManagement.Application.Features.Comments.Commands.UpdateComment;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Profiles;

public class CommentMappingProfile : Profile
{
    public CommentMappingProfile()
    {
        CreateMap<CreateCommentCommand, Comment>();
        CreateMap<UpdateCommentCommand, Comment>();
        CreateMap<Comment, CreateCommentDto>();
    }
}