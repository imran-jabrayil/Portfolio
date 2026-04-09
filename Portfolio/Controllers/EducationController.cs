using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Services.Abstractions;

namespace Portfolio.Controllers;

/// <summary>Serves the dedicated Education &amp; Achievements page.</summary>
public class EducationController(
    ILogger<EducationController> logger,
    IEducationService educationService,
    IAchievementService achievementService) : Controller
{
    private readonly ILogger<EducationController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IEducationService _educationService = educationService ?? throw new ArgumentNullException(nameof(educationService));
    private readonly IAchievementService _achievementService = achievementService ?? throw new ArgumentNullException(nameof(achievementService));

    public async Task<IActionResult> Index()
    {
        Task<IEnumerable<Education>> eduTask = _educationService.GetEducationAsync();
        Task<IEnumerable<Achievement>> achTask = _achievementService.GetAchievementsAsync();

        await Task.WhenAll(eduTask, achTask);

        _logger.LogInformation("Rendering Education page.");
        return View((eduTask.Result, achTask.Result));
    }
}
