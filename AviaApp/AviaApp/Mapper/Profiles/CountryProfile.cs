using AutoMapper;
using AviaApp.Models.Dto;
using Data.Entities;

namespace AviaApp.Mapper.Profiles;

public class CountryProfile : Profile
{
    public CountryProfile()
    {
        CreateMap<Country, CountryDto>();
        CreateMap<CountryDto, Country>();
    }
}