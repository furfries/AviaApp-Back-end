using System;
using System.ComponentModel.DataAnnotations;

namespace AviaApp.Models.Requests;

public class SearchFlightRequest
{
    public Guid? AirportIdFrom { get; set; }

    public Guid? AirportIdTo { get; set; }

    public Guid? CityIdFrom { get; set; }
    
    public Guid? CityIdTo { get; set; }

    public int CabinClassId { get; set; }

    [Required]
    public Guid CountryIdFrom { get; set; }

    [Required]
    public Guid CountryIdTo { get; set; }

    [Required]
    public DateTime FlightDateTime { get; set; }
}