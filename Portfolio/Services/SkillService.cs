using Microsoft.EntityFrameworkCore;
using Portfolio.Database;
using Portfolio.Models;
using Portfolio.Services.Abstractions;

namespace Portfolio.Services;

/// <summary>Retrieves skills grouped and ordered for display.</summary>
public class SkillService(
    ILogger<SkillService> logger,
    IDbContextFactory<PortfolioDbContext> dbContextFactory) : ISkillService
{
    private readonly ILogger<SkillService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IDbContextFactory<PortfolioDbContext> _dbContextFactory =
        dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));

    /// <inheritdoc/>
    public async Task<IEnumerable<Skill>> GetSkillsAsync()
    {
        await using PortfolioDbContext db = await _dbContextFactory.CreateDbContextAsync();

        List<Skill> skills = await db.Skills
            .OrderBy(s => s.Category)
            .ThenBy(s => s.Name)
            .ToListAsync();

        _logger.LogInformation("Retrieved {Count} skills.", skills.Count);
        return skills;
    }
}
