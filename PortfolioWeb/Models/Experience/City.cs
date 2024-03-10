namespace PortfolioWeb.Models;

public class City {
    public string Name { get; set; } = null!;
    public Country Country { get; set; } = null!;
    public string CountryCode { get; set; } = null!;
}