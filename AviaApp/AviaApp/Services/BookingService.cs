using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AviaApp.Models.Requests;
using AviaApp.Services.Contracts;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace AviaApp.Services;

public class BookingService : IBookingService
{
    private readonly AviaAppDbContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<AviaAppUser> _userManager;

    public BookingService(AviaAppDbContext context, IMapper mapper, UserManager<AviaAppUser> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task BookFlightAsync(BookFlightRequest request)
    {
        if (!request.Passengers.Any())
            throw new Exception("Must have at least one passenger");

        if (!_context.Flights.Any(x => x.Id == request.FlightId))
            throw new Exception("This flight does not exist");

        var user = await _userManager.FindByEmailAsync(request.BookedBy);
        if (user is null)
            throw new Exception($"User {request.BookedBy} does not exist");

        var booking = _mapper.Map<Booking>(request);
        booking.Id = Guid.NewGuid();
        booking.BookingDate = DateTime.Now;

        var passengers = _mapper.Map<List<Passenger>>(request.Passengers);
        foreach (var passenger in passengers)
        {
            passenger.BookingId = booking.Id;
        }

        await _context.Passengers.AddRangeAsync(passengers);
        await _context.Bookings.AddAsync(booking);
        await _context.SaveChangesAsync();
    }
}