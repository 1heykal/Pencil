using AutoMapper;
using Pencil.ContentManagement.Application.Features.Blogs.Commands.CreateBlog;
using Pencil.ContentManagement.Application.Features.Blogs.Commands.UpdateBlog;
using Pencil.ContentManagement.Application.Features.Blogs.Queries.GetBlog;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Profiles;

public class BlogMappingProfile : Profile
{
    public BlogMappingProfile()
    {
        CreateMap<CreateBlogCommand, Blog>();
        CreateMap<Blog, CreatedBlogDto>();
        
        CreateMap<Blog, BlogInfoDto>()
            .ForMember(dto => dto.PostsCount, opt => opt.MapFrom(src => src.Posts.Count))
            .ForMember(dto => dto.SubscriptionsCount, opt => opt.MapFrom(src => src.Subscriptions.Count))
            .ForMember(dto => dto.AuthorName, opt => opt.MapFrom(src =>$"{src.Author.FirstName} {src.Author.LastName}"))
            .ForMember(dto => dto.AuthorPhotoPath, opt => opt.MapFrom(src => src.Author.PhotoPath)).ReverseMap();

        CreateMap<UpdateBlogInfoCommand, Blog>();

    }
}