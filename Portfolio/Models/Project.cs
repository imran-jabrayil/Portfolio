namespace Portfolio.Models;

/// <summary>
/// Represents a software project in the portfolio.
/// </summary>
public class Project
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string? GitHubUrl { get; set; }
    public string? LiveUrl { get; set; }

    /// <summary>Comma-separated list of technologies used.</summary>
    public required string Technologies { get; set; }

    public required bool IsFeatured { get; set; }
}
