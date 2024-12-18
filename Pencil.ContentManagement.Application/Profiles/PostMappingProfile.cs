using AutoMapper;
using Pencil.ContentManagement.Application.Features.Blogs.Queries.GetBlogPosts;
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
            .ForMember(dto => dto.BlogUsername, opt => opt.MapFrom(src => src.Blog.Username))
            .ForMember(dto => dto.AuthorName, opt => opt.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}" ))
            .ForMember(dto => dto.AuthorPhotoPath, opt => opt.MapFrom(src => src.Author.PhotoPath))
            .ForMember(dto => dto.AuthorUsername, opt => opt.MapFrom(src => src.Author.Username))
            .ForMember(dto => dto.LikesCount, opt => opt.MapFrom(src => src.Likes.Count))
            .ForMember(dto => dto.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count))
            .ForMember(dto => dto.Tags, opt => opt.MapFrom(src => src.Tags != null || src.Tags.Count == 0 ?src.Tags.Select(tag => tag
            .Name): new List<string>()))
          .ReverseMap();
        
        CreateMap<Post, BlogPostsDto>()
            .ForMember(dto => dto.AuthorUsername, opt => opt.MapFrom(src => src.Author.Username))
            .ForMember(dto => dto.AuthorName, opt => opt.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}" ))
            .ForMember(dto => dto.AuthorPhotoPath, opt => opt.MapFrom(src => src.Author.PhotoPath)).ReverseMap();
        
        CreateMap<CreatePostCommand, Post>()
            .ForMember(dto => dto.Tags, opt => opt.MapFrom(src => src.Tags.Select(tag => new Tag {Name = tag})));
        CreateMap<UpdatePostCommand, Post>();
        CreateMap<Post, CreatePostDto>()
            .ForMember(dto => dto.Tags, opt => opt.MapFrom(src => src.Tags != null || src.Tags.Count == 0 ?src.Tags.Select(tag => tag
                .Name): new List<string>()));
        
        
    }
}