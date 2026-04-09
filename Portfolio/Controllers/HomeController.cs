using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Services.Abstractions;

namespace Portfolio.Controllers;

/// <summary>
/// Serves the portfolio home page (hero + about + section navigation).
/// </summary>
public class HomeController(
    ILogger<HomeController> logger,
    IPersonalInfoService personalInfoService) : Controller
{
    private readonly ILogger<HomeController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IPersonalInfoService _personalInfoService = personalInfoService ?? throw new ArgumentNullException(nameof(personalInfoService));

    public async Task<IActionResult> Index()
    {
        PersonalInfo? info = await _personalInfoService.GetPersonalInfoAsync();

        if (info is null)
        {
            _logger.LogError("PersonalInfo record is missing — cannot render home page.");
            return StatusCode(500, "Portfolio data is not configured.");
        }

        return View(info);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
