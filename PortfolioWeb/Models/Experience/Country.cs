namespace PortfolioWeb.Models;

public class Country {
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public virtual ICollection<City> Cities { get; set; } = null!;
}