using AutoMapper;
using AviaApp.Models.Dto;
using AviaApp.Models.Requests;
using AviaApp.Models.ViewModels;
using Data.Entities;

namespace AviaApp.Mapper.Profiles;

public class FlightProfile : Profile
{
    public FlightProfile()
    {
        CreateMap<Flight, FlightDto>();
        CreateMap<FlightDto, Flight>();

        CreateMap<Flight, FlightViewModel>()
            .ForMember(
                dest => dest.LocationFrom,
                opt =>
                    opt.MapFrom(x => x.AirportFrom))
            .ForMember(
                dest => dest.LocationTo,
                opt =>
                    opt.MapFrom(x => x.AirportTo));


        CreateMap<Airport, LocationViewModel>()
            .ForMember(
                dest => dest.Airport,
                opt =>
                    opt.MapFrom(x => x))
            .ForMember(
                dest => dest.City,
                opt =>
                    opt.MapFrom(x => x.City))
            .ForMember(
                dest => dest.Country,
                opt =>
                    opt.MapFrom(x => x.City.Country));


        CreateMap<City, LocationViewModel>()
            .ForMember(
                dest => dest.City,
                opt =>
                    opt.MapFrom(x => x))
            .ForMember(
                dest => dest.Country,
                opt =>
                    opt.MapFrom(x => x.Country));


        CreateMap<Country, LocationViewModel>()
            .ForMember(
                dest => dest.Country,
                opt =>
                    opt.MapFrom(x => x));


        CreateMap<Airport, AirportViewModel>();
        CreateMap<AirportViewModel, Airport>();

        CreateMap<City, CityViewModel>();
        CreateMap<CityViewModel, City>();

        CreateMap<Country, CountryViewModel>();
        CreateMap<CountryViewModel, Country>();

        CreateMap<AddFlightRequest, Flight>();
    }
}