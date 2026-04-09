namespace Portfolio.Models;

/// <summary>
/// Represents the portfolio owner's personal and contact information.
/// Only one record is expected in the database.
/// </summary>
public class PersonalInfo
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Title { get; set; }
    public required string Bio { get; set; }
    public required string Location { get; set; }
    public string? Email { get; set; }
    public string? GitHubUrl { get; set; }
    public string? LinkedInUrl { get; set; }
    public string? ResumeUrl { get; set; }
}
