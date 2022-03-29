namespace Data.Entities;

public class Airport
{
    public Guid Id { get; set; }

    public Guid CityId { get; set; }

    public string Name { get; set; }

    public City City { get; set; }
}