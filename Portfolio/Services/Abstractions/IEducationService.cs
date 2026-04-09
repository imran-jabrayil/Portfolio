using Portfolio.Models;

namespace Portfolio.Services.Abstractions;

/// <summary>Returns education records ordered by start year descending.</summary>
public interface IEducationService
{
    Task<IEnumerable<Education>> GetEducationAsync();
}
