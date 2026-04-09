using Microsoft.EntityFrameworkCore;
using Portfolio.Database;
using Portfolio.Models;
using Portfolio.Services.Abstractions;

namespace Portfolio.Services;

/// <summary>Retrieves achievements ordered by year descending.</summary>
public class AchievementService(
    ILogger<AchievementService> logger,
    IDbContextFactory<PortfolioDbContext> dbContextFactory) : IAchievementService
{
    private readonly ILogger<AchievementService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IDbContextFactory<PortfolioDbContext> _dbContextFactory =
        dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));

    /// <inheritdoc/>
    public async Task<IEnumerable<Achievement>> GetAchievementsAsync()
    {
        await using PortfolioDbContext db = await _dbContextFactory.CreateDbContextAsync();

        List<Achievement> achievements = await db.Achievements
            .OrderBy(a => a.Id)
            .ToListAsync();

        _logger.LogInformation("Retrieved {Count} achievements.", achievements.Count);
        return achievements;
    }
}
