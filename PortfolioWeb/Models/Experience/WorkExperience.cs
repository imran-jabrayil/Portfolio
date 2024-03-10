namespace PortfolioWeb.Models;

public class WorkExperience {
    public int Id { get; set; }

    public Company Company { get; set; } = null!;
    public string CompanyName { get; set; } = null!;
    
    public Position Position { get; set; } = null!;
    public string PositionName { get; set; } = null!;
    
    public Level? Level { get; set; } 
    
    public City City { get; set; } = null!;
    public string CityName { get; set; } = null!;
    
    public DateTime StartDate { get; set; }
    public DateTime? TerminationDate { get; set; }
}
