using Microsoft.EntityFrameworkCore;
using Portfolio.Database;
using Portfolio.Models;
using Portfolio.Services.Abstractions;

namespace Portfolio.Services;

/// <summary>Retrieves projects ordered by display order, optionally filtered to featured items.</summary>
public class ProjectService(
    ILogger<ProjectService> logger,
    IDbContextFactory<PortfolioDbContext> dbContextFactory) : IProjectService
{
    private readonly ILogger<ProjectService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IDbContextFactory<PortfolioDbContext> _dbContextFactory =
        dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));

    /// <inheritdoc/>
    public async Task<IEnumerable<Project>> GetProjectsAsync(bool featuredOnly = false)
    {
        await using PortfolioDbContext db = await _dbContextFactory.CreateDbContextAsync();

        IQueryable<Project> query = db.Projects.OrderBy(p => p.Id);

        if (featuredOnly)
            query = query.Where(p => p.IsFeatured);

        List<Project> projects = await query.ToListAsync();
        _logger.LogInformation("Retrieved {Count} projects (featuredOnly={FeaturedOnly}).", projects.Count, featuredOnly);
        return projects;
    }
}
