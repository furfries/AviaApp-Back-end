using System;
using System.Reflection.Metadata.Ecma335;

namespace AviaApp.Models.ViewModels;

public class FlightViewModel
{
    public Guid Id { get; set; }

    public string Airplane { get; set; }

    public DateTime DepartureDateTime { get; set; }

    public DateTime ArrivalDateTime { get; set; }

    public bool IsCanceled { get; set; }

    public decimal Price { get; set; }

    public LocationViewModel LocationFrom { get; set; }

    public LocationViewModel LocationTo { get; set; }
}