namespace Portfolio.Models;

/// <summary>
/// Represents an educational qualification.
/// </summary>
public class Education
{
    public required int Id { get; set; }
    public required string Institution { get; set; }
    public required string Degree { get; set; }
    public required string FieldOfStudy { get; set; }
    public required int StartYear { get; set; }
    public int? EndYear { get; set; }
    public string? Description { get; set; }
    public string? Grade { get; set; }
    public string? InstitutionUrl { get; set; }
}
