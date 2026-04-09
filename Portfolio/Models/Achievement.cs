namespace Portfolio.Models;

/// <summary>
/// Represents an award, certificate, or notable achievement.
/// Years is a display string supporting single years ("2021"),
/// ranges ("2022/2023"), and multi-year lists ("2023, 2024, 2025").
/// </summary>
public class Achievement
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Years { get; set; }
    public string? Organization { get; set; }
}
