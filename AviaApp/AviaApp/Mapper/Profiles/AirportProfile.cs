using AutoMapper;
using AviaApp.Models.Dto;
using Data.Entities;

namespace AviaApp.Mapper.Profiles;

public class AirportProfile : Profile
{
    public AirportProfile()
    {
        CreateMap<Airport, AirportDto>();
        CreateMap<AirportDto, Airport>();
    }
}