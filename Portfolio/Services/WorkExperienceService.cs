using Microsoft.EntityFrameworkCore;
using Portfolio.Database;
using Portfolio.Helpers;
using Portfolio.Models;
using Portfolio.Services.Abstractions;

namespace Portfolio.Services;

public class WorkExperienceService(
    ILogger<WorkExperienceService> logger,
    IDbContextFactory<PortfolioDbContext> dbContextFactory): IWorkExperienceService
{
    private readonly ILogger<WorkExperienceService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IDbContextFactory<PortfolioDbContext> _dbContextFactory =
        dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
    
    
    public async Task<IEnumerable<WorkExperienceTimelineBlock>> GetWorkExperienceTimelineBlocksAsync()
    {
        await using PortfolioDbContext dbContext = await _dbContextFactory.CreateDbContextAsync();
        
        List<WorkExperience> workExperienceList = await dbContext.WorkExperience
            .OrderByDescending(w => w.StartDate)
            .ToListAsync();
        _logger.LogInformation("Retrieved {workExperienceList.Count} work experiences.", workExperienceList.Count);

        List<WorkExperienceTimelineBlock> workExperienceTimelineBlocks =
            WorkExperienceHelper.ConvertWorkExperienceToTimelineBlocks(workExperienceList);
        
        return workExperienceTimelineBlocks;
    }
}