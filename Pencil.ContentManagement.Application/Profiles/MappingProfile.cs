using AutoMapper;
using Pencil.ContentManagement.Application.Features.Boxes.Queries;
using Pencil.ContentManagement.Application.Features.Followings.Commands;
using Pencil.ContentManagement.Application.Features.Subscriptions.Commands;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<FollowUserCommand, Following>();
        CreateMap<SubscribeCommand, Subscription>();
        CreateMap<Box, BoxDto>();
    }
}