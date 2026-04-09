using Portfolio.Models;

namespace Portfolio.Services.Abstractions;

/// <summary>Returns achievements ordered by year descending.</summary>
public interface IAchievementService
{
    Task<IEnumerable<Achievement>> GetAchievementsAsync();
}
