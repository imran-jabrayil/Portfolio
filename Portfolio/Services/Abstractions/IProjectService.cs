using Portfolio.Models;

namespace Portfolio.Services.Abstractions;

/// <summary>Returns projects ordered by display order, optionally filtered to featured only.</summary>
public interface IProjectService
{
    Task<IEnumerable<Project>> GetProjectsAsync(bool featuredOnly = false);
}
