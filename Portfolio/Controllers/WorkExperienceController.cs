using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Services.Abstractions;

namespace Portfolio.Controllers;

public class WorkExperienceController(
    ILogger<WorkExperienceController> logger,
    IWorkExperienceService workExperienceService) : Controller
{
    private readonly ILogger<WorkExperienceController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IWorkExperienceService _workExperienceService =
        workExperienceService ?? throw new ArgumentNullException(nameof(workExperienceService));

    public async Task<IActionResult> Index()
    {
        IEnumerable<WorkExperienceTimelineBlock> blocks = await _workExperienceService.GetWorkExperienceTimelineBlocksAsync();
        
        List<WorkExperienceTimelineBlock> blocksList = blocks as List<WorkExperienceTimelineBlock> ?? blocks.ToList();
        
        _logger.LogInformation("Retrieved {blocksList.Count} work experience timeline blocks", blocksList.Count);
        
        return View(blocksList);
    }
}