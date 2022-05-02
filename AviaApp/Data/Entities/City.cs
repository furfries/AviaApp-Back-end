namespace Data.Entities;

public class City
{
    public Guid Id { get; set; }

    public Guid CountryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public Country? Country { get; set; }

    public IList<Airport>? Airports { get; set; }
}