namespace Portfolio.Models;

/// <summary>
/// Represents a technical skill grouped by category.
/// </summary>
public class Skill
{
    public required int Id { get; set; }

    /// <summary>Category such as "Languages", "Frameworks", "Databases", "DevOps", "Observability".</summary>
    public required string Category { get; set; }

    public required string Name { get; set; }
}
