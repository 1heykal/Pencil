using AutoMapper;
using Pencil.ContentManagement.Application.Features.Account.Commands.UpdateProfile;
using Pencil.ContentManagement.Application.Features.Account.Queries.GetProfile;
using Pencil.ContentManagement.Application.Features.Auth.Commands.RegisterUser;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Profiles;

public class ApplicationUserMappingProfile : Profile
{
    public ApplicationUserMappingProfile()
    {
        CreateMap<RegisterUserCommand, ApplicationUser>();
        
        CreateMap<UpdateProfileCommand, ApplicationUser>().ReverseMap();

        CreateMap<ApplicationUser, ProfileDetailsDto>()
            .ForMember(dto => dto.FollowersCount, opt => opt.MapFrom(src => src.Followers.Count))
            .ForMember(dto => dto.FollowingCount, opt => opt.MapFrom(src => src.Following.Count));


    }
}