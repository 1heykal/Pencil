using AutoMapper;
using Pencil.ContentManagement.Application.Features.Posts.Commands.CreatePost;
using Pencil.ContentManagement.Application.Features.Posts.Commands.UpdatePost;
using Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Profiles;

public class PostMappingProfile : Profile
{
    public PostMappingProfile()
    {
        CreateMap<Post, PostsDto>()
            .ForMember(dto => dto.BlogName, opt => opt.MapFrom(src => src.Blog.Name))
            .ForMember(dto => dto.BlogPhotoPath, opt => opt.MapFrom(src => src.Blog.PhotoPath))
            .ForMember(dto => dto.AuthorName, opt => opt.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}" ))
            .ForMember(dto => dto.AuthorPhotoPath, opt => opt.MapFrom(src => src.Author.PhotoPath))
            .ForMember(dto => dto.LikesCount, opt => opt.MapFrom(src => src.Likes.Count))
            .ForMember(dto => dto.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count)).ReverseMap();

        
        CreateMap<Post, BlogPostsDto>()
            .ForMember(dto => dto.AuthorName, opt => opt.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}" ))
            .ForMember(dto => dto.AuthorPhotoPath, opt => opt.MapFrom(src => src.Author.PhotoPath)).ReverseMap();
        
        CreateMap<CreatePostCommand, Post>();
        CreateMap<UpdatePostCommand, Post>();
        CreateMap<Post, CreatePostDto>();
    }
}