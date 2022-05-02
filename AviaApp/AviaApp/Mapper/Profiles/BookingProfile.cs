using AutoMapper;
using AviaApp.Models.Requests;
using Data.Entities;

namespace AviaApp.Mapper.Profiles;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<BookFlightRequest, Booking>();
        CreateMap<Booking, BookFlightRequest>();

        CreateMap<PassengerRequest, Passenger>();
        CreateMap<Passenger, PassengerRequest>();
    }
}