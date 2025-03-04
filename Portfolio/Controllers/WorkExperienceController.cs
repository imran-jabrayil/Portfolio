using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Database;
using Portfolio.Models;

namespace Portfolio.Controllers;

public class WorkExperienceController(
    ILogger<WorkExperienceController> logger,
    IDbContextFactory<PortfolioDbContext> dbContextFactory) : Controller
{
    private readonly ILogger<WorkExperienceController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IDbContextFactory<PortfolioDbContext> _dbContextFactory =
        dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));

    public async Task<IActionResult> Index()
    {
        await using PortfolioDbContext dbContext = await _dbContextFactory.CreateDbContextAsync();
        List<WorkExperience> workExperienceList = await dbContext.WorkExperience
            .OrderByDescending(w => w.StartDate)
            .ToListAsync();
        _logger.LogInformation($"Retrieved {workExperienceList.Count} work experiences.");
        return View(workExperienceList);
    }
}