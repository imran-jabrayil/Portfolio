using Microsoft.AspNetCore.Mvc;
using Portfolio.Services.Abstractions;

namespace Portfolio.Controllers;

/// <summary>Serves the dedicated Projects page.</summary>
public class ProjectsController(
    ILogger<ProjectsController> logger,
    IProjectService projectService) : Controller
{
    private readonly ILogger<ProjectsController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IProjectService _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));

    public async Task<IActionResult> Index()
    {
        var projects = await _projectService.GetProjectsAsync(featuredOnly: false);
        _logger.LogInformation("Rendering Projects page.");
        return View(projects);
    }
}
