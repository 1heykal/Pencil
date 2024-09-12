using AutoMapper;
using Pencil.ContentManagement.Application.Features.Account.Commands.UpdateProfile;
using Pencil.ContentManagement.Application.Features.Auth.Commands.RegisterUser;
using Pencil.ContentManagement.Application.Features.Blogs.Commands;
using Pencil.ContentManagement.Application.Features.Blogs.Commands.CreateBlog;
using Pencil.ContentManagement.Application.Features.Blogs.Queries.GetBlog;
using Pencil.ContentManagement.Application.Features.Posts.Commands.CreatePost;
using Pencil.ContentManagement.Application.Features.Posts.Commands.UpdatePost;
using Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Post, PostsDto>()
            .ForMember(dto => dto.BlogName, opt => opt.MapFrom(src => src.Blog.Name))
            .ForMember(dto => dto.BlogPhotoPath, opt => opt.MapFrom(src => src.Blog.PhotoPath))
            .ForMember(dto => dto.AuthorName, opt => opt.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}" ))
            .ForMember(dto => dto.AuthorPhotoPath, opt => opt.MapFrom(src => src.Author.PhotoPath)).ReverseMap();

        CreateMap<RegisterUserCommand, ApplicationUser>();
        CreateMap<CreatePostCommand, Post>();
        CreateMap<UpdatePostCommand, Post>();

        CreateMap<CreateBlogCommand, Blog>();
        CreateMap<Blog, CreatedBlogDto>();
        
        CreateMap<Blog, BlogInfoDto>()
            .ForMember(dto => dto.PostsCount, opt => opt.MapFrom(src => src.Posts.Count))
            .ForMember(dto => dto.SubscriptionsCount, opt => opt.MapFrom(src => src.Subscriptions.Count))
            .ForMember(dto => dto.AuthorName, opt => opt.MapFrom(src =>$"{src.Author.FirstName} {src.Author.LastName}"))
            .ForMember(dto => dto.AuthorPhotoPath, opt => opt.MapFrom(src => src.Author.PhotoPath)).ReverseMap();
        
        CreateMap<Post, CreatePostDto>();

        CreateMap<UpdateProfileCommand, ApplicationUser>().ReverseMap();

    }
}