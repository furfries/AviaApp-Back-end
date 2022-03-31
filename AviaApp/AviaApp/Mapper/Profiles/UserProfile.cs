using AutoMapper;
using AviaApp.Models.Dto;
using Data.Entities;

namespace AviaApp.Mapper.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<AviaAppUser, UserDto>()
            .ForMember(dest => dest.Roles, act => act.Ignore());
    }
}