using AutoMapper;
using AviaApp.Models.Dto;
using Data.Entities;

namespace AviaApp.Mapper.Profiles;

public class CityProfile : Profile
{
    public CityProfile()
    {
        CreateMap<City, CityDto>();
        CreateMap<CityDto, City>();
    }
}