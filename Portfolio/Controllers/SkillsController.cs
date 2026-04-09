using Microsoft.AspNetCore.Mvc;
using Portfolio.Services.Abstractions;

namespace Portfolio.Controllers;

/// <summary>Serves the dedicated Skills page.</summary>
public class SkillsController(
    ILogger<SkillsController> logger,
    ISkillService skillService) : Controller
{
    private readonly ILogger<SkillsController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly ISkillService _skillService = skillService ?? throw new ArgumentNullException(nameof(skillService));

    public async Task<IActionResult> Index()
    {
        var skills = await _skillService.GetSkillsAsync();
        _logger.LogInformation("Rendering Skills page.");
        return View(skills);
    }
}
