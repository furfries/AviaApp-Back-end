using AutoMapper;
using AviaApp.Models.Dto;
using Data.Entities;

namespace AviaApp.Mapper.Profiles;

public class LocationProfile : Profile
{
    public LocationProfile()
    {
        CreateMap<Country, CountryDto>();
        CreateMap<CountryDto, Country>();
    }
}