namespace AviaApp.Models.ViewModels;

public class LocationViewModel
{
    public CountryViewModel Country { get; set; }

    public CityViewModel City { get; set; }

    public AirportViewModel Airport { get; set; }
}