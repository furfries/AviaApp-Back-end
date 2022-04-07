using AutoMapper;
using AviaApp.Models.Dto;
using Data.Entities;

namespace AviaApp.Mapper.Profiles;

public class FlightProfile : Profile
{
    public FlightProfile()
    {
        CreateMap<Flight, FlightDto>();
        CreateMap<FlightDto, Flight>();
    }
}