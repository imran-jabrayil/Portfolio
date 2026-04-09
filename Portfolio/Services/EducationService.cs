using Microsoft.EntityFrameworkCore;
using Portfolio.Database;
using Portfolio.Models;
using Portfolio.Services.Abstractions;

namespace Portfolio.Services;

/// <summary>Retrieves education records ordered by start year descending.</summary>
public class EducationService(
    ILogger<EducationService> logger,
    IDbContextFactory<PortfolioDbContext> dbContextFactory) : IEducationService
{
    private readonly ILogger<EducationService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IDbContextFactory<PortfolioDbContext> _dbContextFactory =
        dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));

    /// <inheritdoc/>
    public async Task<IEnumerable<Education>> GetEducationAsync()
    {
        await using PortfolioDbContext db = await _dbContextFactory.CreateDbContextAsync();

        List<Education> education = await db.Education
            .OrderByDescending(e => e.StartYear)
            .ToListAsync();

        _logger.LogInformation("Retrieved {Count} education records.", education.Count);
        return education;
    }
}
