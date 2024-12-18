using AutoMapper;
using Pencil.ContentManagement.Application.Features.Comments.Commands.CreateComment;
using Pencil.ContentManagement.Application.Features.Comments.Commands.UpdateComment;
using Pencil.ContentManagement.Application.Features.Comments.Queries.GetComments;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Profiles;

public class CommentMappingProfile : Profile
{
    public CommentMappingProfile()
    {
        CreateMap<CreateCommentCommand, Comment>();
        
        CreateMap<UpdateCommentCommand, Comment>();
        CreateMap<Comment, CreateCommentDto>()
            .ForMember(dto => dto.AuthorName,
                opt => opt.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}"))
            .ForMember(dto => dto.AuthorUserName, opt => opt.MapFrom(src => src.Author.Username))
            .ForMember(dto => dto.AuthorPhotoPath, opt => opt.MapFrom(src => src.Author.PhotoPath));

        CreateMap<Comment, CommentsDto>()
            .ForMember(dto => dto.AuthorName,
                opt => opt.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}"))
            .ForMember(dto => dto.AuthorUserName, opt => opt.MapFrom(src => src.Author.Username))
            .ForMember(dto => dto.AuthorPhotoPath, opt => opt.MapFrom(src => src.Author.PhotoPath))
            .ForMember(dto => dto.LikesCount, opt => opt.MapFrom(src => src.Likes.Count));

    }
}