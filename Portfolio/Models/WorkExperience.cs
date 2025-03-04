namespace Portfolio.Models;

public class WorkExperience
{
    public required long Id { get; set; }
    public required string Company { get; set; }
    public required string CompanyUrl { get; set; }
    public required string Position { get; set; }
    public required string Country { get; set; }
    public required string City { get; set; }
    public required DateTime StartDate { get; set; }
    public DateTime? TerminationDate { get; set; }
}